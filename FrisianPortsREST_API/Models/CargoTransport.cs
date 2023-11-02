using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrisianPortsREST_API.Models
{
    public class CargoTransport
    {

        public int Cargo_Transport_Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Frequency { get; set; }

        [Required]
        public DateTime Date_Started { get; set; }

        [Required]
        public int AddedById { get; set; }

        [Required]
        public int Route_Id { get; set; }

    }
}
