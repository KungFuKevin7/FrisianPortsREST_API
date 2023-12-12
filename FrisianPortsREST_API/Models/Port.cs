using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FrisianPortsREST_API.Models
{
    public class Port
    {
        private int _portId;

        private string? _portName;

        private string? _portLocation;

        private string? _latitude;

        private string? _longitude; 



        public int PortId 
        { 
            get { return _portId; } 
            set { _portId = value; }
        }

        [Required]
        [MaxLength(30)]
        public string? PortName 
        {
            get { return _portName; } 
            set { _portName = value; } 
        }

        [Required]
        [MaxLength(30)]
        public string? PortLocation
        {
            get { return _portLocation; }
            set { _portLocation = value; }
        }

        [Required]
        [MaxLength(20)]
        public string? Latitude 
        { 
            get { return _latitude; }
            set { _latitude = value; }
        }


        [Required]
        [MaxLength(20)]
        public string? Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
    }
}
