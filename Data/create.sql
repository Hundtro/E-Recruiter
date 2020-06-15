--run in database data.db with sqlite

CREATE TABLE "Reminder" (
	"Id"	  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Value"	  TEXT NOT NULL,
	"Created" TEXT NOT NULL
);

CREATE TABLE "Worker" (
 	"Id"	  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Name"	  TEXT NOT NULL,
	"Surname" TEXT NOT NULL
}
