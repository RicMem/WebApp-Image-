using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DB
{
    public class TestUsers
    {
        [Key]
        public int UserID { get; set; }
        public string LogInName { get; set; }
        public string PasswordEncryptedText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
