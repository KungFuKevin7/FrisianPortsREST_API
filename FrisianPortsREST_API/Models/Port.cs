using System.Text.Json.Serialization;

namespace FrisianPortsREST_API.Models
{
    public class Port
    {
        public int Port_Id { get; set; }

        public string Port_Name { get; set; }

        public string Port_Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

    }
}
