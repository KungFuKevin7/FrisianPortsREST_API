using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Province
    {
        public int Province_Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Province_Name { get; set; }
    }
}
