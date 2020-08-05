--create tables

CREATE TABLE "Users" (
    "Id"        INTEGER PRIMARY KEY AUTOINCREMENT,    
    "Login"     TEXT NOT NULL,
    "Password"  TEXT NOT NULL,
    "FullName"  TEXT NOT NULL,
    "CanEdit"   INTEGER DEFAULT 0,
    "CanConfig" INTEGER DEFAULT 0
);

CREATE TABLE "Reminders" (
    "Id"        INTEGER PRIMARY KEY AUTOINCREMENT,
    "Text"      TEXT NOT NULL,
    "Created"   TEXT NOT NULL,
    "CreatedBy" INTEGER NOT NULL,
    FOREIGN KEY(CreatedBy) REFERENCES Users(Id)
);

CREATE TABLE "EmailTemplates" (
    "Id"      INTEGER PRIMARY KEY AUTOINCREMENT,
    "Title"   TEXT NOT NULL,
    "Message" TEXT NOT NULL
);

CREATE TABLE Candidates (
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
    FOREIGN KEY(CreatedBy) REFERENCES Users(Id)
);

CREATE TABLE JobTitles (
    "Id"	    INTEGER PRIMARY KEY AUTOINCREMENT,
    "Title"         TEXT NOT NULL,
    "DefaultSalary" TEXT NOT NULL,
    "Description"   TEXT,
    "CreatedBy"     INTEGER NOT NULL,
    FOREIGN KEY(CreatedBy) REFERENCES Users(Id)
);

CREATE TABLE Experience (
    "Id"            INTEGER PRIMARY KEY AUTOINCREMENT,
    "CandidateId"   INTEGER NOT NULL,
    "Name"          TEXT NOT NULL,
    "Title"         TEXT NOT NULL,
    "StartDate"     TEXT NOT NULL,
    "EndDate"       TEXT NOT NULL,
    FOREIGN KEY(CandidateId) REFERENCES Candidates(Id)
);

CREATE TABLE HireSteps (
    "Id"            INTEGER PRIMARY KEY AUTOINCREMENT,
    "JobTitleId"    INTEGER NOT NULL,
    "Order"         INTEGER,
    "Name"          TEXT NOT NULL,
    "Description"   TEXT,
    FOREIGN KEY(JobTitleId) REFERENCES JobTitles(Id)
);

CREATE TABLE HireProcess (
    "Id"          INTEGER PRIMARY KEY AUTOINCREMENT,
    "CandidateId" INTEGER NOT NULL,
    "JobTitleId"  INTEGER NOT NULL,
    "StepId"      INTEGER NOT NULL,
    "Comments"    TEXT,
    FOREIGN KEY(CandidateId) REFERENCES Candidates(Id),
    FOREIGN KEY(JobTitleId) REFERENCES JobTitles(Id),
    FOREIGN KEY(StepId) REFERENCES HireSteps(Id)
);

--Create default values

INSERT INTO main.Users ("Login", "Password", "FullName", "CanEdit", "CanConfig") VALUES ("admin", "admin", "admin", 1, 1);  
INSERT INTO main.JobTitles ("Title", "DefaultSalary", "CreatedBy") VALUES ('General', 0, (SELECT Id FROM main.Users WHERE Login ='admin'));
INSERT INTO main.HireSteps ("JobTitleId", "Order", "Name") VALUES ((SELECT Id FROM main.JobTitles WHERE Title = 'General'), 1, 'Default');
