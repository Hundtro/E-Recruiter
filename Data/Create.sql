--create tables

CREATE TABLE User (
    "Id"        INTEGER PRIMARY KEY AUTOINCREMENT,    
    "Login"     TEXT NOT NULL,
    "Password"  TEXT NOT NULL,
    "FullName"  TEXT NOT NULL,
    "CanEdit"   INTEGER DEFAULT 0,
    "CanConfig" INTEGER DEFAULT 0
);

CREATE TABLE Reminder (
    "Id"        INTEGER PRIMARY KEY AUTOINCREMENT,
    "Text"      TEXT NOT NULL,
    "Created"   TEXT NOT NULL,
    "CreatedBy" INTEGER NOT NULL,
    FOREIGN KEY(CreatedBy) REFERENCES User(Id)
);

CREATE TABLE EmailTemplate (
    "Id"      INTEGER PRIMARY KEY AUTOINCREMENT,
    "Title"   TEXT NOT NULL,
    "Message" TEXT NOT NULL
);

CREATE TABLE Candidate (
    "Id"           INTEGER PRIMARY KEY AUTOINCREMENT,
    "Name"         TEXT NOT NULL,
    "Surname"      TEXT NOT NULL,
    "BirthDate"    TEXT NOT NULL,
    "Gender"       TEXT NOT NULL,
    "WantedSalary" TEXT,
    "Email"        TEXT,
    "MobilePhone"  TEXT,
    "HomeOffice"   INTEGER NOT NULL,
    "ExWorker"     INTEGER NOT NULL,
    "Photo"        BLOB,
    "CVFile"       BLOB,
    "CreatedBy"    INTEGER NOT NULL,
    FOREIGN KEY(CreatedBy) REFERENCES User(Id)
);

CREATE TABLE JobTitle (
    "Id"            INTEGER PRIMARY KEY AUTOINCREMENT,
    "Title"         TEXT NOT NULL,
    "DefaultSalary" TEXT NOT NULL,
    "Description"   TEXT,
    "CreatedBy"     INTEGER NOT NULL,
    FOREIGN KEY(CreatedBy) REFERENCES User(Id)
);

CREATE TABLE Experience (
    "Id"            INTEGER PRIMARY KEY AUTOINCREMENT,
    "CandidateId"   INTEGER NOT NULL,
    "Name"          TEXT NOT NULL,
    "Title"         TEXT NOT NULL,
    "StartDate"     TEXT NOT NULL,
    "EndDate"       TEXT NOT NULL,
    FOREIGN KEY(CandidateId) REFERENCES Candidate(Id)
);

CREATE TABLE HireStep (
    "Id"            INTEGER PRIMARY KEY AUTOINCREMENT,
    "JobTitleId"    INTEGER NOT NULL,
    "OrderNo"       INTEGER NOT NULL,
    "Name"          TEXT NOT NULL,
    "Description"   TEXT,
    FOREIGN KEY(JobTitleId) REFERENCES JobTitle(Id)
);

CREATE TABLE HireProcess (
    "Id"          INTEGER PRIMARY KEY AUTOINCREMENT,
    "CandidateId" INTEGER NOT NULL,
    "JobTitleId"  INTEGER NOT NULL,
    "StepId"      INTEGER NOT NULL,
    "Comments"    TEXT,
    FOREIGN KEY(CandidateId) REFERENCES Candidate(Id),
    FOREIGN KEY(JobTitleId) REFERENCES JobTitle(Id),
    FOREIGN KEY(StepId) REFERENCES HireStep(Id)
);

--Create default values

INSERT INTO main.User ("Login", "Password", "FullName", "CanEdit", "CanConfig") VALUES ("admin", "admin", "Admin", 1, 1);  
INSERT INTO main.JobTitle ("Title", "DefaultSalary", "CreatedBy") VALUES ('General', 0, (SELECT Id FROM main.User WHERE Login ='admin'));
INSERT INTO main.HireStep ("JobTitleId", "OrderNo", "Name") VALUES ((SELECT Id FROM main.JobTitle WHERE Title = 'General'), 1, 'Default');
