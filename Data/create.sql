--run in database data.db with sqlite
--create tables
CREATE TABLE "Reminder" (
	"Id"	  INTEGER PRIMARY KEY AUTOINCREMENT,
	"Value"	  TEXT NOT NULL,
	"Created" TEXT
);

CREATE TABLE Employee (
	"Id"           INTEGER PRIMARY KEY AUTOINCREMENT,
    "Name"         TEXT NOT NULL,
    "Surname"      TEXT NOT NULL,
	"BirthDate"    TEXT NOT NULL,
	"Gender"       TEXT NOT NULL,
	"WantedSalary" TEXT,
	"Email"        TEXT,
	"MobilePhone"  TEXT,
	"HomeOffice"   INTEGER NOT NULL,
	"ExWorker"     INTEGER NOT NULL
);

CREATE TABLE JobTitle (
	"Id"	        INTEGER PRIMARY KEY AUTOINCREMENT,
	"Title"         TEXT NOT NULL,
	"DefaultSalary" TEXT NOT NULL,
	"Description"   TEXT
);

CREATE TABLE Experience (
	"Id"			INTEGER PRIMARY KEY AUTOINCREMENT,
	"EmployeeId"    INTEGER NOT NULL,
	"Name"          TEXT NOT NULL,
	"Title"         TEXT NOT NULL,
	"StartDate"     TEXT NOT NULL,
	"EndDate"       TEXT NOT NULL,
	FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
);

CREATE TABLE HireStep (
	"Id"			INTEGER PRIMARY KEY AUTOINCREMENT,
	"JobTitleId"    INTEGER NOT NULL,
	"Order"         INTEGER,
	"Name"          TEXT NOT NULL,
	"Description"   TEXT,
	FOREIGN KEY(JobTitleId) REFERENCES JobTitle(Id)
);

CREATE TABLE HireProcess (
	"Id" 			INTEGER PRIMARY KEY AUTOINCREMENT,
	"EmployeeId"    INTEGER NOT NULL,
	"JobTitleId"    INTEGER NOT NULL,
	"StepId"        INTEGER NOT NULL,
	"Comments"      TEXT,
	FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
	FOREIGN KEY(JobTitleId) REFERENCES JobTitle(Id),
	FOREIGN KEY(StepId) REFERENCES HireStep(Id)
);

--Create default values
INSERT INTO main.JobTitle ("Title", "DefaultSalary") VALUES ('General', 0);
INSERT INTO main.HireStep ("JobTitleId", "Order", "Name") VALUES ((SELECT Id FROM main.JobTitle WHERE Title = 'General'), 1, 'Default');

