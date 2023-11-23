namespace FrisianPortsREST_API.DTO
{
    //Merging of Port and CargoTransport
    public class GoodsFlowDto
    {
        public int Cargo_Transport_Id { get; set; }
        public string? Departure_Location { get; set; }
        public string? Arrival_Location { get; set; }
        public string? Frequency { get; set; }
        public int Total_Weight { get; set; }
    }
}
