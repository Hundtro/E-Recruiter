SELECT
    Id AS Id,
    Name AS Name,
    Surname AS Surname,
    BirthDate AS BirthDate,
    Gender AS Gender,
    WantedSalary AS WantedSalary,
    Email AS Email,
    MobilePhone AS MobilePhone,
    HomeOffice AS HomeOffice,
    ExWorker AS ExWorker,
    Photo AS Photo,
    CVFile AS CVFile,
    CreatedBy AS CreatedBy
FROM main.Candidate
WHERE name LIKE '%?name?%' AND surname LIKE '%?surname?%'
