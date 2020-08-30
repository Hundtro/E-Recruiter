SELECT
    Id AS Id,
    JobTitleId AS JobTitleId, 
    OrderNo AS OrderNo,
    Name AS Name,
    Description AS Description
FROM main.HireStep
WHERE JobTitleId = ?id?
