using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Users
    {
        public int User_Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set;}

        [Required]
        [MaxLength(50)]
        public string? Password { get; set;}

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set;}

        [Required]
        [MaxLength(50)]
        public string? SurName { get; set;}

        [Required]
        public bool Permission_Add_Cargo { get; set;}
    }
}
