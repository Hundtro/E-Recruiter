SELECT
    Name,
    Title,
    StartDate,
    EndDate
FROM main.Experience
WHERE CandidateId = ?id?
