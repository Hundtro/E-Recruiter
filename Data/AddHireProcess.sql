INSERT INTO main.HireProcess (CandidateId, JobTitleId, StepId)
VALUES (?candidateid?, ?jobtitleid?, (SELECT Id FROM main.HireStep WHERE OrderNo = 1 AND JobTitleId = ?jobtitleid?))
