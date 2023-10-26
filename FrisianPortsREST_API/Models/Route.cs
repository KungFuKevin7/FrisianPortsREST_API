namespace FrisianPortsREST_API.Models
{
    public class Route
    {
        public int Route_Id { get; set; }

        public int Departure_Port_Id { get; set; }

        public int Arrival_Port_Id { get; set; }
    }
}
