using System.Text.Json.Serialization;

namespace PopugJira.Models
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    
    public enum Role
    {
        admin,
        manager,
        bookkeeper,
        programmer
    }
}