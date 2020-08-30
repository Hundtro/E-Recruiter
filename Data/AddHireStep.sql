INSERT INTO main.HireStep (JobTitleId, OrderNo, Name, Description)
VALUES (?jobTitleId?, 0, '?name?', '?description?');

UPDATE main.HireStep
SET OrderNo = (SELECT MAX(OrderNo)+1 FROM main.HireStep WHERE JobTitleId = ?jobTitleId?)
WHERE JobTitleId = ?jobTitleId? AND Name = '?name?';
