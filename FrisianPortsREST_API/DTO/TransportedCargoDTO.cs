namespace FrisianPortsREST_API.DTO
{
    /// <summary>
    /// DTO used to transfer:
    /// CargoType and total transported weight
    /// </summary>
    public class TransportedCargoDTO
    {
        public string? Cargo_Type_Name { get; set; }

        public int? Transported_Weight { get; set; }
    }
}
