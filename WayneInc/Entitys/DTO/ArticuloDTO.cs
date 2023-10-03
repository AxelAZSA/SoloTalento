using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace WayneInc.Entitys.DTO
{
    public class ArticuloDTO
    {
        public int idArticulo { get; set; }
        [Required]
        public string codigo { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public string imagen { get; set; }
    }
}
