namespace FrisianPortsREST_API.DTO
{
    public class GoodsFlowDto
    {
        public string? DepartureLocation { get; set; }
        public string? DestinationLocation { get; set; }
        public string? Frequency { get; set; }
        public int TotalWeight { get; set; }
    }
}
