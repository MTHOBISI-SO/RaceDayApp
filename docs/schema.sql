-- ===================================================
-- RaceDay System - Database Schema Script
-- ===================================================

-- 1. Create Role Table
CREATE TABLE Role (
    roleID INT IDENTITY(1,1) PRIMARY KEY,
    roleName NVARCHAR(50) NOT NULL UNIQUE
);

-- 2. Create User Table
CREATE TABLE [User] (
    userID INT IDENTITY(1,1) PRIMARY KEY,
    roleID INT NOT NULL,
    fullName NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) NOT NULL UNIQUE,
    passwordHash NVARCHAR(255) NOT NULL,
    createdAT DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_User_Role FOREIGN KEY (roleID) REFERENCES Role(roleID)
);

-- 3. Create Event Table
CREATE TABLE Event (
    eventID INT IDENTITY(1,1) PRIMARY KEY,
    organiserID INT NOT NULL,
    title NVARCHAR(150) NOT NULL,
    description NVARCHAR(MAX) NULL,
    eventDate DATETIME NOT NULL,
    location NVARCHAR(200) NOT NULL,
    CONSTRAINT FK_Event_Organiser FOREIGN KEY (organiserID) REFERENCES [User](userID)
);

-- 4. Create Category Table
CREATE TABLE Category (
    categoryID INT IDENTITY(1,1) PRIMARY KEY,
    eventID INT NOT NULL,
    categoryName NVARCHAR(100) NOT NULL,
    distanceKM DECIMAL(5,2) NOT NULL,
    maxParticipants INT NOT NULL DEFAULT 100,
    CONSTRAINT FK_Category_Event FOREIGN KEY (eventID) REFERENCES Event(eventID) ON DELETE CASCADE
);

-- 5. Create Enrolment Table
CREATE TABLE Enrolment (
    enrolmentID INT IDENTITY(1,1) PRIMARY KEY,
    userID INT NOT NULL,
    categoryID INT NOT NULL,
    enrolmentDate DATETIME DEFAULT GETDATE(),
    status NVARCHAR(20) DEFAULT 'Confirmed',
    CONSTRAINT FK_Enrolment_User FOREIGN KEY (userID) REFERENCES [User](userID),
    CONSTRAINT FK_Enrolment_Category FOREIGN KEY (categoryID) REFERENCES Category(categoryID),
    CONSTRAINT UQ_User_Category UNIQUE (userID, categoryID)
);

-- 6. Create Result Table
CREATE TABLE Result (
    resultID INT IDENTITY(1,1) PRIMARY KEY,
    enrolmentID INT NOT NULL UNIQUE,
    finishTime TIME NOT NULL,
    position INT NOT NULL,
    CONSTRAINT FK_Result_Enrolment FOREIGN KEY (enrolmentID) REFERENCES Enrolment(enrolmentID)
);

-- ===================================================
-- SEED SAMPLE DATA
-- ===================================================

-- Roles
INSERT INTO Role (roleName) VALUES ('Organiser'), ('Participant');

-- Users (2 Organisers, 2 Participants)
INSERT INTO [User] (roleID, fullName, email, passwordHash) VALUES 
(1, 'Alice Organiser', 'alice@raceday.com', 'HashedPassword123'),
(1, 'Bob Organiser', 'bob@raceday.com', 'HashedPassword123'),
(2, 'Charlie Runner', 'charlie@gmail.com', 'HashedPassword123'),
(2, 'Diana Sprinter', 'diana@gmail.com', 'HashedPassword123');

-- Events (3 Events)
INSERT INTO Event (organiserID, title, description, eventDate, location) VALUES 
(1, 'City Marathon 2026', 'Annual city road marathon event.', '2026-10-15 06:00:00', 'Central Park'),
(1, 'Mountain Trail Run', 'Scenic off-road trail running challenge.', '2026-11-05 07:00:00', 'Green Valley Trail'),
(2, 'Coastal Night Walk', 'Fun night walk event along the coast.', '2026-12-01 19:00:00', 'Ocean Drive');

-- Categories
INSERT INTO Category (eventID, categoryName, distanceKM, maxParticipants) VALUES 
(1, 'Full Marathon', 42.20, 500),
(1, 'Half Marathon', 21.10, 1000),
(2, 'Trail 15K', 15.00, 200),
(3, 'Fun Walk 5K', 5.00, 300);

-- Enrolments
INSERT INTO Enrolment (userID, categoryID) VALUES 
(3, 1),
(4, 1),
(3, 3);

-- Results
INSERT INTO Result (enrolmentID, finishTime, position) VALUES 
(1, '03:15:22', 1),
(2, '03:28:10', 2);

SELECT * FROM Role;
SELECT * FROM [User];