using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class Candidate 
    {
        public string Id { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string WantedSalary { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string HomeOffice { get; set; }
        public string ExWorker { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile CVFile { get; set; }
        public string PhotoData { get; set; }
        public string CVFileData { get; set; }
        public string CreatedBy { get; set; }		
    }
}
