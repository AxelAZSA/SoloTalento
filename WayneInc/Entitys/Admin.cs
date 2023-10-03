﻿using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
    }
}
