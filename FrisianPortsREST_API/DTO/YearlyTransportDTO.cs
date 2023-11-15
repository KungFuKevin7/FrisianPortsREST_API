namespace FrisianPortsREST_API.DTO
{
    public class YearlyTransportDTO
    {
        public int? Year { get; set; }

        public List<TransportedCargoDTO>? Transported {  get; set; }
    }
}
