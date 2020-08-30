--add
INSERT INTO main.HireStep (JobTitleId, "Order", Name, Description)
VALUES (2, 0, 'Default7', '');

UPDATE main.HireStep
SET "Order" = (SELECT MAX("Order")+1 FROM main.HireStep WHERE JobTitleId = 2)
WHERE JobTitleId = 2 AND Name = 'Default7';

--update after delete
UPDATE main.HireStep
SET "Order" = "Order"-1
WHERE "Order">(SELECT "Order" FROM main.HireStep WHERE Id = 7);

DELETE FROM main.HireStep WHERE Id = 7;
