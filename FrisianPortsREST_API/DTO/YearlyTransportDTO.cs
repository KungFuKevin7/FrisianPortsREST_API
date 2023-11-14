namespace FrisianPortsREST_API.DTO
{
    public class YearlyTransportDTO
    {
        public int? Year { get; set; }

        public TransportedCargoDTO? Transported {  get; set; }
    }
}
