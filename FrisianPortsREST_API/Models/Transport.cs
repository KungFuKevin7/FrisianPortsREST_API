using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Transport
    {
        private int _transportId;

        private int? _cargoTransportId;

        private DateTime? _departureDate;


        public int TransportId 
        {
            get { return _transportId; }
            set { _transportId = value; } 
        }

        [Required]
        public int? CargoTransportId
        {
            get { return _cargoTransportId; }
            set { _cargoTransportId = value; }
        }

        [Required]
        public DateTime? DepartureDate
        {
            get { return _departureDate; }
            set { _departureDate = value; }
        }
    }
}
