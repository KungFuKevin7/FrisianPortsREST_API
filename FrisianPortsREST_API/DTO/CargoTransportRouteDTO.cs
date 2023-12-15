namespace FrisianPortsREST_API.DTO
{
    public class CargoTransportRouteDTO
    {
        public int Route_Id { get; set; }

        public int Departure_Port_Id { get; set; }
        
        public int Arrival_Port_Id { get; set; }

        public int Cargo_Transport_Id { get; set; }
        
        public string? Frequency { get; set; }
        
        public DateTime? Date_Started { get; set; }
        
        public int Added_By_Id { get; set; }
    }
}
