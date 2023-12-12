using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Users
    {
        private int _userId;

        private string? _email;

        private string? _password;

        private string? _firstname;

        private string? _surname;

        private bool _permissionAddCargo;


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Required]
        [MaxLength(50)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required]
        [MaxLength(50)]
        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value;}
        }

        [Required]
        [MaxLength(50)]
        public string SurName
        {
            get { return _surname; }
            set { _surname = value; }
        }

        [Required]
        public bool PermissionAddCargo
        {
            get { return _permissionAddCargo; }
            set { _permissionAddCargo = value; }
        }
    }
}
