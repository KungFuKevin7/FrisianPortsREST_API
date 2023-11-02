using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Route
    {
        public int Route_Id { get; set; }

        [Required]
        public int Departure_Port_Id { get; set; }

        [Required]
        public int Arrival_Port_Id { get; set; }
    } 
}
