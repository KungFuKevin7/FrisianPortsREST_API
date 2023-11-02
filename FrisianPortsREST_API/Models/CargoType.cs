using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class CargoType
    {
        public int Cargo_Type_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Cargo_Type_Name { get; set; }
    }
}
