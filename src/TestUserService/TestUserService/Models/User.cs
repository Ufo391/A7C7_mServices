using System.ComponentModel.DataAnnotations;

namespace TestUserService.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
