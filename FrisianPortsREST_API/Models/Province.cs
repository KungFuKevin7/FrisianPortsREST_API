using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.Models
{
    public class Province
    {
        private int _provinceId;

        private string _provinceName;


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
    }
}
