using System;

namespace WayneInc.Entitys.DTO
{
    public class CompraDTO
    {
        public int idCliente { get; set; }
        public string articuloDescripcion { get; set; }
        public DateTime fecha { get; set; }
        public string imagen { get; set; }
    }
}
