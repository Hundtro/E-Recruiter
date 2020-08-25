SELECT
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
    CVFile AS CVFile
FROM main.Candidate
WHERE Id = ?id?
