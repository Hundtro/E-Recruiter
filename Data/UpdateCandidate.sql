UPDATE main.Candidate
SET Name = '?name?',
	Surname = '?surname?',
	BirthDate = '?birthdate?',
	Gender = '?gender?',
	WantedSalary = '?wantedsalary?',
	Email = '?email?',
	MobilePhone = '?mobilephone?',
	HomeOffice = '?homeoffice?',
	ExWorker = '?exworker?',
	Photo = '?photo?',
	CVFile = '?cvfile?'
WHERE Id = ?id?
