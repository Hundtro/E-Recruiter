SELECT
    hireStep.Id AS StepId
FROM main.HireProcess hireProcess
    JOIN main.HireStep hireStep
    ON hireProcess.JobTitleId = hireStep.JobTitleId
WHERE OrderNo = (SELECT OrderNo FROM main.HireStep WHERE Id = hireProcess.StepId) + 1
AND hireProcess.Id = ?id?
