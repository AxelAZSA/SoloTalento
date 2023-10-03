using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys.DTO
{
    public class ItemDto
    {
        public int idCarritoItem { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public decimal subtotal { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precioU { get; set; }
        [Required]
        public string imagen { get; set; }
        [Required]
        public int idArticulo { get; set; }
    }
}
