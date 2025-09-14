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

def update_clubs():
    connection = connect_to_db()

    if connection:
        try:
            cursor = connection.cursor()

            # Clear table
            cursor.execute("""
                TRUNCATE TABLE Clubs;
                """
            )

            # Recalculate clubs leaderboard
            cursor.execute("""
                INSERT INTO Clubs (ClubTag, Users, RankScore, Position, AvgScore)
                SELECT q.ClubTag, q.Users, q.RankScore, @position := @position + 1 AS Position, CAST(RankScore AS DECIMAL) / Users AS AvgScore
                FROM (
                    SELECT U.ClubTag, COUNT(1) AS Users, SUM(L.RankScore) AS RankScore
                    FROM (
                        SELECT Name, MAX(Timestamp) AS LatestTimestamp
                        FROM LeaderboardLastWeek
                        WHERE Season = 's8' AND Timestamp >= NOW() - INTERVAL 1 HOUR
                        GROUP BY Name
                    ) AS LatestLeaderboard
                    INNER JOIN LeaderboardLastWeek L ON LatestLeaderboard.Name = L.Name AND LatestLeaderboard.LatestTimestamp = L.Timestamp AND L.Season = 's8'
                    INNER JOIN Users U ON L.Name = U.Name
                    WHERE U.ClubTag IS NOT NULL AND TRIM(U.ClubTag) <> ''
                    GROUP BY U.ClubTag
                    ORDER BY RankScore DESC
                ) AS q
                JOIN (SELECT @position := 0) AS pos_init;
                """
            )
            print("Updated clubs table.")
            connection.commit()
            cursor.close()
        except Error as e:
            print(f"Error: {e}")
        finally:
            connection.close()
    else:
        print("no connection to the databse.")

if __name__ == "__main__":
    while True:
        update_clubs()
        time.sleep(3600) # Every hour