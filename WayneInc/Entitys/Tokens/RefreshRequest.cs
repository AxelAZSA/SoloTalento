using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys.Tokens
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
