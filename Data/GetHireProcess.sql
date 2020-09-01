SELECT
    candidate.Id AS Id,
    candidate.Name || ' ' || candidate.Surname AS Candidate,
    title.Title AS JobTitle,
    step.Name AS Step,
    process.Comments AS Comments
FROM main.HireProcess process
JOIN main.Candidate candidate
ON process.CandidateId = candidate.Id
JOIN main.JobTitle title
ON process.JobTitleId = title.Id
JOIN main.HireStep step
ON process.StepId = step.Id
WHERE process.Id = ?id?
