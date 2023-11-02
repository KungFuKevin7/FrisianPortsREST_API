﻿using FrisianPortsREST_API.Models;
using System.ComponentModel.DataAnnotations;

namespace FrisianPortsREST_API.DTO
{
    public class PortGoodsFlowDto
    {
        public string? PortName { get; set; }
        public string? PortLocation { get; set; }
        public List<Cargo>? CargoImport { get; set; }
        public List<Cargo>? CargoExport { get; set; }
    }
}
