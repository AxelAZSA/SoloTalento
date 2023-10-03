using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class CarritoItem
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal subtotal { get; set; }
        [Required]
        public int idCarrito { get; set; }

    }
}
