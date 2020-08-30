SELECT
     title.Id AS Id,
     title.Title AS Title,
     title.DefaultSalary AS DefaultSalary,
     title.Description AS Description,
     user.FullName AS CreatedBy
FROM main.JobTitle title
JOIN main.User user
ON title.CreatedBy = user.Id
WHERE title.Id = ?id?
