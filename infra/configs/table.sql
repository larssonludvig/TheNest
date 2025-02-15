CREATE TABLE leaderboard (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    RankPosition INT NOT NULL,
    ChangeAmount INT NOT NULL,
    -- SteamName VARCHAR(255),
    -- XboxName VARCHAR(255),
    -- PsnName VARCHAR(255),
    LeagueNumber INT NOT NULL,
    League VARCHAR(255) NOT NULL,
    RankScore INT NOT NULL,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
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

INSERT INTO Specializations (name, heavy, medium, light) VALUES
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

INSERT INTO Weapons (name, heavy, medium, light) VALUES
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
('M26_Matter', FALSE, FALSE, TRUE);


CREATE TABLE Gadgets (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Heavy BOOLEAN,
    Medium BOOLEAN,
    Light BOOLEAN
);

INSERT INTO Gadgets (name, heavy, medium, light) VALUES
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
('Lockbolt_Launcher', TRUE, FALSE, FALSE);
