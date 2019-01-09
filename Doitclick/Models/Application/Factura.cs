using System;
using System.ComponentModel.DataAnnotations;

namespace Doitclick.Models.Application
{
    public class Factura
    {
        [MaxLength(100)]
        public string Id { get; set; }
        public DateTime FechaFacturacion { get; set; }
        public float ValorFactura { get; set; }
        public CotizacionExterno CotizacionFactura { get; set; }
        public EntidadFacturacion PagadorFactura { get; set; }
        public bool Cerrada { get; set; }
    }
}