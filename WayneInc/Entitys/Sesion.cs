using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WayneInc.Entitys
{
    public class Sesion
    {

        [Key]
        public int id { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public int idCliente { get; set; }
    }
}
