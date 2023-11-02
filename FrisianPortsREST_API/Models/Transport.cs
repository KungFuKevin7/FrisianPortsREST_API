using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Transport
    {
        public int Transport_Id { get; set; }

        [Required]
        public int Cargo_Transport_Id { get; set; }

        [Required]
        public DateTime Departure_Date { get; set; }
    }
}
