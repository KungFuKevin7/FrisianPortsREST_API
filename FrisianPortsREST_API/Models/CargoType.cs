using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class CargoType
    {
        private int _cargoTypeId;

        private string? _cargoTypeName;

        public int CargoTypeId
        {
            get { return _cargoTypeId; }
            set { _cargoTypeId = value; }
        }

        [Required]
        [MaxLength(50)]
        public string CargoTypeName
        {
            get { return _cargoTypeName; }
            set { _cargoTypeName = value; }
        }
    }
}
