SELECT
    Id,
    Text,
    Created
FROM main.Reminder
WHERE CreatedBy = '?userId?'

