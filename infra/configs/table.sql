CREATE TABLE leaderboard (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    rank_pos INT NOT NULL,
    change_pos INT NOT NULL,
    steamName VARCHAR(255),
    xboxName VARCHAR(255),
    leagueNumber INT NOT NULL,
    league VARCHAR(255) NOT NULL,
    rankScore INT NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
