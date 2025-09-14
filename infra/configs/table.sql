CREATE TABLE LeaderboardS8 (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    RankPosition INT NOT NULL,
    ChangeAmount INT NOT NULL,
    LeagueNumber INT NOT NULL,
    RankScore INT NOT NULL,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Season VARCHAR(255)
);

CREATE TABLE Clubs (
    ClubTag VARCHAR(5) PRIMARY KEY,
    Users INT DEFAULT 0,
    RankScore INT DEFAULT 0,
    Position INT NOT NULL,
    AvgScore Decimal
);

CREATE TABLE Users (
    Name VARCHAR(255) PRIMARY KEY,
    SteamName VARCHAR(255),
    XboxName VARCHAR(255),
    PsnName VARCHAR(255)
);

CREATE TABLE Ranks (
    
);

CREATE TABLE Builds (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

INSERT INTO Builds (name) VALUES
('Light'),
('Medium'),
('Heavy');

CREATE TABLE Specializations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Heavy BOOLEAN,
    Medium BOOLEAN,
    Light BOOLEAN
);

INSERT INTO Specializations (Name, Heavy, Medium, Light) VALUES
('Charge_N_Slam', TRUE, FALSE, FALSE),
('Goo_Gun', TRUE, FALSE, FALSE),
('Mesh_Shield', TRUE, FALSE, FALSE),
('Winch_Claw', TRUE, FALSE, FALSE),
('Guardian_Turret', FALSE, TRUE, FALSE),
('Healing_Beam', FALSE, TRUE, FALSE),
('Dematerializer', FALSE, TRUE, FALSE),
('Cloaking_Device', FALSE, FALSE, TRUE),
('Evasive_Dash', FALSE, FALSE, TRUE),
('Grappling_Hook', FALSE, FALSE, TRUE);

CREATE TABLE Weapons (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Heavy BOOLEAN,
    Medium BOOLEAN,
    Light BOOLEAN
);

INSERT INTO Weapons (Name, Heavy, Medium, Light) VALUES
('M60', TRUE, FALSE, FALSE),
('Lewis_Gun', TRUE, FALSE, FALSE),
('Flamethrower', TRUE, FALSE, FALSE),
('MGL32', TRUE, FALSE, FALSE),
('SA1216', TRUE, FALSE, FALSE),
('Sledgehammer', TRUE, FALSE, FALSE),
('KS-23', TRUE, FALSE, FALSE),
('Spear', TRUE, FALSE, FALSE),
('50_Akimbo', TRUE, FALSE, FALSE),
('ShAK-50', TRUE, FALSE, FALSE),
('AKM', FALSE, TRUE, FALSE),
('FCAR', FALSE, TRUE, FALSE),
('CL-40', FALSE, TRUE, FALSE),
('R.357', FALSE, TRUE, FALSE),
('Model_1887', FALSE, TRUE, FALSE),
('Riot_Shield', FALSE, TRUE, FALSE),
('FAMAS', FALSE, TRUE, FALSE),
('Dual_Blades', FALSE, TRUE, FALSE),
('Pike-556', FALSE, TRUE, FALSE),
('Cerberus_12GA', FALSE, TRUE, FALSE),
('V9S', FALSE, FALSE, TRUE),
('93R', FALSE, FALSE, TRUE),
('M11', FALSE, FALSE, TRUE),
('XP-54', FALSE, FALSE, TRUE),
('SH1900', FALSE, FALSE, TRUE),
('LH1', FALSE, FALSE, TRUE),
('SR-84', FALSE, FALSE, TRUE),
('Dagger', FALSE, FALSE, TRUE),
('Sword', FALSE, FALSE, TRUE),
('Throwing_Knives', FALSE, FALSE, TRUE),
('Recurve_Bow', FALSE, FALSE, TRUE),
('M26_Matter', FALSE, FALSE, TRUE),
('ARN-220', FALSE, FALSE, TRUE),
('CB-01_Repeater', FALSE, TRUE, FALSE),
('M134_Minigun', TRUE, FALSE, FALSE);


CREATE TABLE Gadgets (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Heavy BOOLEAN,
    Medium BOOLEAN,
    Light BOOLEAN
);

INSERT INTO Gadgets (Name, Heavy, Medium, Light) VALUES
('Breach_Charge', FALSE, FALSE, TRUE),
('Flashbang', TRUE, TRUE, TRUE),
('Frag_Grenade', TRUE, TRUE, TRUE),
('Gas_Grenade', TRUE, TRUE, TRUE),
('Gateway', FALSE, FALSE, TRUE),
('Goo_Grenade', TRUE, TRUE, TRUE),
('Glitch_Grenade', FALSE, FALSE, TRUE),
('Smoke_Grenade', TRUE, TRUE, TRUE),
('Sonar_Grenade', FALSE, FALSE, TRUE),
('Stun_Gun', FALSE, FALSE, TRUE),
('Thermal_Vision', FALSE, FALSE, TRUE),
('Tracking_Dart', FALSE, FALSE, TRUE),
('Pyro_Grenade', TRUE, TRUE, TRUE),
('Vanishing_Bomb', FALSE, FALSE, TRUE),
('Thermal_Bore', FALSE, FALSE, TRUE),
('Gravity_Vortex', FALSE, FALSE, TRUE),
('APS_Turret', FALSE, TRUE, FALSE),
('Defibrillator', FALSE, TRUE, FALSE),
('Explosive_Mine', TRUE, TRUE, FALSE),
('Gas_Mine', FALSE, TRUE, FALSE),
('Glitch_Trap', FALSE, TRUE, FALSE),
('Jump_Pad', FALSE, TRUE, FALSE),
('Zipline', FALSE, TRUE, FALSE),
('Proximity_Sensor', TRUE, TRUE, FALSE),
('C4', TRUE, FALSE, FALSE),
('Pyro_Mine', TRUE, FALSE, FALSE),
('Barricade', TRUE, FALSE, FALSE),
('RPG-7', TRUE, FALSE, FALSE),
('Dome_Shield', TRUE, FALSE, FALSE),
('Anti-Gravity_Cube', TRUE, FALSE, FALSE),
('Lockbolt_Launcher', TRUE, FALSE, FALSE),
('Nullifier', FALSE, FALSE, TRUE),
('H+_Infuser', FALSE, FALSE, TRUE),
('Breach_Drill', FALSE, TRUE, FALSE),
('Healing_Emitter', TRUE, FALSE, FALSE);



SELECT *
FROM Leaderboard L
WHERE Season = 's5' AND MAX(Timestamp)
GROUP BY Name
INNER JOIN Users U ON L.Name = U.Name;





INSERT INTO Clubs (ClubTag, Users, RankScore, Position, AvgScore)
SELECT q.ClubTag, q.Users, q.RankScore, @position := @position + 1 AS Position, CAST(RankScore AS DECIMAL) / Users AS AvgScore
FROM (
    SELECT U.ClubTag, COUNT(1) AS Users, SUM(L.RankScore) AS RankScore
    FROM (
        SELECT Name, MAX(Timestamp) AS LatestTimestamp
        FROM Leaderboard
        WHERE Season = 's5' AND Timestamp >= NOW() - INTERVAL 1 HOUR
        GROUP BY Name
    ) AS LatestLeaderboard
    INNER JOIN Leaderboard L ON LatestLeaderboard.Name = L.Name AND LatestLeaderboard.LatestTimestamp = L.Timestamp AND L.Season = 's5'
    INNER JOIN Users U ON L.Name = U.Name
    WHERE U.ClubTag IS NOT NULL AND TRIM(U.ClubTag) <> ''
    GROUP BY U.ClubTag
    ORDER BY RankScore DESC
) AS q
JOIN (SELECT @position := 0) AS pos_init;
