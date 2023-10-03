using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    public class Login
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
    }
}
