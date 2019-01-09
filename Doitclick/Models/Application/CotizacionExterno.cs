using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Doitclick.Models.Application
{
    public class CotizacionExterno
    {
        [MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(30), Required]
        public string RutPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string NumeroTicket { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string EntidadSolicitante { get; set; }
        public string OrdenFolio { get; set; }
        public string OrdenImagen { get; set; }
        [MaxLength(500)]
        public string Resumen { get; set; }
        public float PrecioTotal { get; set; }
        public bool RequiereRetiro { get; set; }
        public string DireccionRetiro { get; set; }
        public ICollection<ItemCotizar> ItemsCotizar { get; set; }

    }
}