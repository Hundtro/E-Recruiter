SELECT
    Id AS Id,
    JobTitleId AS JobTitleId, 
    "Order",
    Name AS Name,
    Description AS Description
FROM main.HireStep
WHERE JobTitleId = ?id?
