namespace FrisianPortsREST_API.DTO
{
    /// <summary>
    /// DTO used to transfer:
    /// CargoType and total transported weight
    /// </summary>
    public class TransportedCargoDTO
    {
        public string? Cargo_Type_Name { get; set; }

        public int Transported_Weight { get; set; }

        public TransportedCargoDTO(string cargoTypeName, int transportedWeight)
        {
            Cargo_Type_Name = cargoTypeName;
            Transported_Weight = transportedWeight;
        }
        public TransportedCargoDTO() { }
    }
}
