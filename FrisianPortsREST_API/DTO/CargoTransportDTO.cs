using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.DTO
{
    public class CargoTransportDTO
    {
        public int? CargoId { get; set; }

        public string? CargoDescription { get; set; }
        [Required]
        public int? WeightInTonnes { get; set; }
        [Required]
        public int? CargoTypeId { get; set; }
        [Required]
        public int? TransportId { get; set; }
        [Required]
        public int? CargoTransportId { get; set; }

        public DateTime DepartureDate { get; set;}
    }
}
