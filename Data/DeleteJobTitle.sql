DELETE FROM main.HireStep
WHERE JobTitleId = ?id?;

DELETE FROM main.JobTitle
WHERE Id = ?id?;
