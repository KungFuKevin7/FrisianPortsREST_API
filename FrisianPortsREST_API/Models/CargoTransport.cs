using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrisianPortsREST_API.Models
{
    public class CargoTransport
    {

        private int _cargoTransportId;

        private string? _Frequency;

        private DateTime? _dateStarted;

        private int? _addedById;

        private int? _routeId;
        

        public int CargoTransportId 
        { 
            get { return _cargoTransportId; }
            set { _cargoTransportId = value;}
        }

        [Required]
        [MaxLength(20)]
        public string Frequency 
        {
            get { return _Frequency; }
            set { _Frequency = value; }
        }

        [Required]
        public DateTime? DateStarted 
        {
            get { return _dateStarted; }
            set { _dateStarted = value; }
        }

        [Required]
        public int? AddedById
        {
            get { return _addedById; }
            set { _addedById = value; }
        }

        [Required]
        public int? RouteId
        {
            get { return _routeId; }
            set { _routeId = value; }
        }
    }
}
