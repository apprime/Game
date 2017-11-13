namespace Web.Models
{
    public class Credentials : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool LoginFailed { get; set; }
    }
}
