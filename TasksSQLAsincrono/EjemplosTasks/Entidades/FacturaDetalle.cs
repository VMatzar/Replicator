using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class FacturaDetalle
    {
        int idDetalle { get; set; }
        int precioUnitario { get; set; }
        int cantidad { get; set; }
        int totalLinea { get; set; }
        int totalIva { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }
        int idFactura { get; set; }
        int idProducto { get; set; }

        public FacturaDetalle(int idDetalle, int precioUnitario, int cantidad,
            int totalLinea, int totalIva, string estadoRegistro, DateTime fechaActualizacion, int idFactura, int idProducto)
        {
            this.idDetalle = idDetalle;
            this.precioUnitario = precioUnitario;
            this.cantidad = cantidad;
            this.totalLinea = totalLinea;
            this.totalIva = totalIva;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
            this.idFactura = idFactura;
            this.idProducto = idProducto;
        }
    }
}
