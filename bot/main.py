# ...existing code...
from twitchAPI.chat import Chat, EventData, ChatMessage, ChatSub, ChatCommand
from twitchAPI.type import AuthScope, ChatEvent
from twitchAPI.oauth import UserAuthenticator
from twitchAPI.twitch import Twitch
from datetime import datetime
import asyncio
import random
import re
import os
import requests
import time

APP_ID = ''
APP_SECRET = ''
USER_SCOPE = [AuthScope.CHAT_READ, AuthScope.CHAT_EDIT]
TARGET_CHANNEL = ''
TOKEN = ''
REFRESH_TOKEN = ''

CHAT = None
_pending_clip = {}

async def on_message(msg: ChatMessage):
    text = (msg.text or "").strip()
    user_name = (getattr(msg, "user", None).name if getattr(msg, "user", None) else "").lower()
    room = None

    if hasattr(msg, "room") and msg.room:
        room = getattr(msg.room, "name", msg.room)
    elif hasattr(msg, "channel") and msg.channel:
        room = getattr(msg.channel, "name", msg.channel)
    else:
        room = TARGET_CHANNEL
    room = str(room).lower()

    if re.match(r'^\s*!bot\b', text, re.IGNORECASE):
        if CHAT:
            await CHAT.send_message(room, "Shhhhh, I am not here...")
        return

    m = re.match(r'^\s*!ruby\b', text, re.IGNORECASE)
    if m:
        requester = (getattr(msg, "user", None).name if getattr(msg, "user", None) else "unknown")
        try:
            response = requests.get(f"https://api.plopparn.tv/leaderboard/ruby/string", timeout=5)
            if response.status_code == 200:
                reply = response.text.strip()
            else:
                reply = f"@{requester}, could not find ruby."
        except Exception as e:
            reply = f"@{requester}, error when fetching ruby."
            print(f"[{room}] Exception while fetching rank: {e}")
        if CHAT:
            await CHAT.send_message(room, reply)
        return

    m = re.match(r'^\s*!rank\b(?:\s+(.*))?$', text, re.IGNORECASE)
    if m:
        user = (m.group(1) or "").strip()
        if not user:
            user = "twitch_plopparn"
        requester = (getattr(msg, "user", None).name if getattr(msg, "user", None) else "unknown")
        print(f"[{room}] Rank command from {requester!s}: user='{user}'")
        try:
            response = requests.get(f"https://api.plopparn.tv/leaderboard/{user}/string", timeout=5)
            if response.status_code == 200:
                reply = response.text.strip()
            else:
                reply = f"@{requester}, could not fetch rank for {user}."
        except Exception as e:
            reply = f"@{requester}, error fetching rank for {user}."
            print(f"[{room}] Exception while fetching rank: {e}")
        if CHAT:
            await CHAT.send_message(room, reply)
        return


    m = re.match(r'^\s*!clip\b(?:\s+(.*))?$', text, re.IGNORECASE)
    if m:
        note = (m.group(1) or "").strip()
        requester = (getattr(msg, "user", None).name if getattr(msg, "user", None) else "unknown")
        print(f"[{room}] Clip command from {requester!s}: note='{note}' — waiting 10s for Streamelements link...")
        loop = asyncio.get_running_loop()
        fut = loop.create_future()
        _pending_clip[room] = (fut, note, requester)

        try:
            link = await asyncio.wait_for(fut, timeout=10.0)
            print(f"[{room}] Received clip link from Streamelements: {link!s} for note: '{note}'")
        except asyncio.TimeoutError:
            print(f"[{room}] Timed out waiting for Streamelements link for clip command by {requester}.")
        finally:
            _pending_clip.pop(room, None)

        return

    if user_name == "streamelements":
        pending = _pending_clip.get(room)
        if pending:
            fut, note, requester = pending
            if fut.done():
                return
            url_match = re.search(r'(https?://\S+)', text)
            if url_match:
                link = url_match.group(1).rstrip('.,)')
                if not fut.done():
                    fut.set_result(link)
                    print(f"[{room}] Streamelements link captured: {link} (for note: {note})")
                    try:
                        response = requests.put(
                            "https://api.plopparn.tv/notes/bot",
                            json={
                                "Description": note,
                                "Username": requester,
                                "ClipURI": link
                            },
                            timeout=5
                        )
                        if response.status_code == 200:
                            print(f"[{room}] Successfully sent clip note to API.")
                            if CHAT:
                                response = requests.get(f"https://api.plopparn.tv/notes/{requester}", timeout=5)
                                if response.status_code == 200:
                                    used_count = response.json()
                                    reply = f"@{requester}, Thank you for clipping! Your clips have been used {used_count} times."
                                    await CHAT.send_message(room, reply)
                        else:
                            print(f"[{room}] Failed to send clip note to API. Status code: {response.status_code}")
                    except Exception as e:
                        print(f"[{room}] Exception while sending clip note to API: {e}")

async def on_ready(event: EventData):
    await event.chat.join_room(TARGET_CHANNEL)
    print(f"Joined channel: {TARGET_CHANNEL}")
    print("Bot is ready!")

async def run_bot():
    global CHAT
    bot = await Twitch(APP_ID, APP_SECRET)
    await bot.set_user_authentication(TOKEN, USER_SCOPE, REFRESH_TOKEN)

    chat = await Chat(bot)
    CHAT = chat

    chat.register_event(ChatEvent.READY, on_ready)
    chat.register_event(ChatEvent.MESSAGE, on_message)

    chat.start()

    try:
        while True:
            await asyncio.sleep(1)
    finally:
        chat.stop()
        await bot.close()

if __name__ == "__main__":
    asyncio.run(run_bot())