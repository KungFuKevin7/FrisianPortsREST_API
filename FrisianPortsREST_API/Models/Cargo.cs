using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Cargo
    {
        public int Cargo_Id { get; set; }

        [MaxLength(500)]
        public string? Cargo_Description { get; set; }
        
        [Required]
        public int? Weight_In_Tonnes { get; set; }

        [Required]
        public int? Cargo_Type_Id { get; set; }

        [Required]
        public int? Transport_Id { get; set; }
    }
}
