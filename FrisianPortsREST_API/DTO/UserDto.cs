﻿namespace FrisianPortsREST_API.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public bool Permission_Add_Cargo { get; set; }

    }
}
