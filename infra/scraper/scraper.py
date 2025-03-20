import requests
import time
import mysql.connector
from mysql.connector import Error
from datetime import datetime

def connect_to_db():
    try:
        connection = mysql.connector.connect(
            user='root',
            host='10.43.114.2',
            password='<pass>',
            port='3306',
            database='thefinals'
        )
        if connection.is_connected():
            print("Successfully connected to the database")
        return connection
    except Error as e:
        print(f"Error: {e}")
        return None

def insert_data(data):
    connection = connect_to_db()

    if connection:
        try:
            cursor = connection.cursor()
            timestamp = datetime.now()
            for item in data.get('data'):
                cursor.execute("""
                    INSERT INTO LeaderboardS6 (Name, RankPosition, ChangeAmount, LeagueNumber, RankScore, Timestamp, Season)
                    VALUES (%s, %s, %s, %s, %s, %s, %s)
                    """, (
                        item.get('name'), item.get('rank'), item.get('change'), item.get('leagueNumber'), item.get('rankScore'), timestamp, "s6"
                    )
                )
                cursor.execute("""
                    INSERT INTO Users (Name, SteamName, XboxName, PsnName, ClubTag)
                    VALUES (%s, %s, %s, %s, %s)
                    ON DUPLICATE KEY UPDATE
                        SteamName = VALUES(SteamName),
                        XboxName = VALUES(XboxName),
                        PsnName = VALUES(PsnName),
                        ClubTag = VALUES(ClubTag);
                    """, (
                        item.get('name'), item.get('steamName'), item.get('xboxName'), item.get('psnName'), item.get('clubTag')
                    )
                )
            print("Inserted scraped values.")
            connection.commit()
            cursor.close()
        except Error as e:
            print(f"Error: {e}")
        finally:
            connection.close()
    else:
        print("no connection to the databse.")

def scrape_api():
    url = "https://api.the-finals-leaderboard.com/v1/leaderboard/s6/crossplay"
    print("Scraping started!")
    response = requests.get(url)
    if response.status_code == 200:
        data = response.json()
        insert_data(data)
        print(f"Inserted data to database at {datetime.now()}")
    else:
        print(f"Failed to retrieve data: {response.status_code}")

if __name__ == "__main__":
    while True:
        scrape_api()
        time.sleep(900)