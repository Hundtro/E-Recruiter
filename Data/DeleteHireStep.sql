UPDATE main.HireStep
SET OrderNo = OrderNo - 1
WHERE OrderNo > (SELECT OrderNo FROM main.HireStep WHERE Id = ?id?);

DELETE FROM main.HireStep WHERE Id = ?id?;
