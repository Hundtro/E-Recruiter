SELECT
    Id AS Id,
    Name AS Name,
    Surname AS Surname,
    Photo AS Photo
FROM main.Candidate
WHERE name LIKE '%?name?%' AND surname LIKE '%?surname?%'
