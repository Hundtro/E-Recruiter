SELECT
    Id AS Id,
    Title AS Title,
    DefaultSalary AS DefaultSalary,
    Description AS Description,
    CreatedBy AS CreatedBy
FROM main.JobTitle
WHERE Id = ?id?
