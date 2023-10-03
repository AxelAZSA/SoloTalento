using System;
using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    //Compra
    public class ArticuloCliente
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idCliente {  get; set; }
        [Required]
        public int idArticulo {  get; set; }
        [Required]
        public DateTime fecha { get; set; }
    }
}
