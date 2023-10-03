using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Tienda
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string sucursal {  get; set; }
        [Required]
        public string direccion { get; set; }
    }
}
