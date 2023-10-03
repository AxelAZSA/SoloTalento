using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Cliente
    {

        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string direccion {  get; set; }
    }
}
