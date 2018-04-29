 
namespace DTO.Core
{
    
    public class LoginResult
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string URI { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyId { get; set; }
        public string Email { get; set; }
        public string AttachmentId { get; set; }
        public byte? TimeZone { get; set; }
    }
}
