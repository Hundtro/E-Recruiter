SELECT
    Id AS Id,
    Login AS Login,
    Password AS Password,
    FullName AS FullName,
    CanEdit AS CanEdit,
    CanConfig AS CanConfig
FROM main.User
WHERE Id = ?id?

