using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Province
    {
        private int _provinceId;

        private string _provinceName;

        private string _latitude;

        private string _longitude;

        public int ProvinceId 
        {
            get { return _provinceId; } 
            set { _provinceId = value; } 
        }

        [Required]
        [MaxLength(30)]
        public string ProvinceName 
        {
            get { return _provinceName; }
            set { _provinceName = value; }
        }

        public string Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
    }
}
