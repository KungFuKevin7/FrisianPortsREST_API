namespace FrisianPortsREST_API.DTO
{
    //Merging of Port and CargoTransport
    public class GoodsFlowDto
    {
        public int CargoTransportId { get; set; }
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
        public string? Frequency { get; set; }
        public int TotalWeight { get; set; }
    }
}
