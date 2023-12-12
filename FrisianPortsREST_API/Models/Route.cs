using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Route
    {
        private int _routeId;

        private int? _departurePortId;

        private int? _arrivalPortId;



        public int RouteId 
        {
            get { return _routeId; }
            set { _routeId = value; }
        }

        [Required]
        public int? DeparturePortId
        {
            get { return _departurePortId; }
            set { _departurePortId = value; }
        }

        [Required]
        public int? ArrivalPortId
        {
            get { return _arrivalPortId; }
            set { _arrivalPortId = value; }
        }
    } 

}
