using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FrisianPortsREST_API.Models
{
    public class Port
    {
        public int Port_Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Port_Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Port_Location { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Latitude { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Longitude { get; set; }

    }
}
