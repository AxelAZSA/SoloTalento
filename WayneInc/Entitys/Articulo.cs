using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Articulo
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string codigo {  get; set; }
        [Required]
        public string descripcion {  get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public byte[] imagen { get; set; }
        [Required]
        public int stock {  get; set; }
    }
}
