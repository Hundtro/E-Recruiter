SELECT
    process.Id AS Id,
    candidate.Name || ' ' || candidate.Surname AS Candidate,
    title.Title AS Title
FROM main.HireProcess process
    JOIN main.JobTitle title
ON process.JobTitleId = title.Id
    JOIN main.Candidate candidate
ON process.CandidateId = candidate.Id
    WHERE Candidate like '%?candidate?%' AND Title like '%?title?%'
