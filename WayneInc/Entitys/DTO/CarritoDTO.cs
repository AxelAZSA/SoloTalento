using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys.DTO
{
    public class CarritoDTO
    {
        [Required]
        public int idCarrito { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public List<ItemDto>? items { get; set; }
    }
}
