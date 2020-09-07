UPDATE main.User
SET Login = '?login?',
    Password = '?password?',
    FullName = '?fullname?',
    CanEdit = ?canedit?,
    CanConfig = ?canconfig?
WHERE Id = ?id?


