using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Carrito
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idCliente { get; set; }
        [Required]
        public decimal total { get; set; }
    }
}
