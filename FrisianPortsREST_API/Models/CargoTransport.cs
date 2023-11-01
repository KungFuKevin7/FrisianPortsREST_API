using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrisianPortsREST_API.Models
{
    public class CargoTransport
    {

        public int Cargo_Transport_Id { get; set; }

        public string Frequency { get; set; }

        public DateTime Date_Started { get; set; }

        public int AddedById { get; set; }

        public int Route_Id { get; set; }

    }
}
