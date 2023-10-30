using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrisianPortsREST_API.Models
{
    public class CargoTransport
    {

        public int CargoTransportId { get; set; }

        public string Frequency { get; set; }

        public DateTime DateStarted { get; set; }

        public int AddedById { get; set; }

        public int Route_Id { get; set; }

    }
}
