using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Cargo
    {
        private int _cargoId;

        private string? _cargoDescription;

        private int? _weightInTonnes;

        private int? _cargoTypeId;

        private int? _transportId;


        public int CargoId 
        {
            get { return _cargoId; }
            set { _cargoId = value; }
        }

        [MaxLength(500)]
        public string CargoDescription
        {
            get { return _cargoDescription; }
            set { _cargoDescription = value; }
        }

        [Required]
        public int? WeightInTonnes
        {
            get { return _weightInTonnes; }
            set { _weightInTonnes = value; }
        }

        [Required]
        public int? CargoTypeId
        {
            get { return _cargoTypeId; }
            set { _cargoTypeId = value; }
        }

        [Required]
        public int? TransportId
        {
            get { return _transportId; }
            set { _transportId = value; }
        }
    }
}
