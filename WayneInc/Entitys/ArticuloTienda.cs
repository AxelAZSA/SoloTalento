using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace WayneInc.Entitys
{
    //Stock
    public class ArticuloTienda
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idTienda { get; set; }
        [Required]
        public int idArticulo {  get; set; }
        [Required]
        public DateTime fecha { get; set; }
    }
}
