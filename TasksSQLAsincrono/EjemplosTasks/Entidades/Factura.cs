using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class Factura
    {
        int idFactura { get; set; }
        DateTime fecha { get; set; }
        int totalFactura { get; set; }
        int totalIva { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }
        int idCliente { get; set; }

        public Factura(int idFactura, DateTime fecha, int totalFactura,
            int totalIva, string estadoRegistro, DateTime fechaActualizacion, int idCliente)
        {
            this.idFactura = idFactura;
            this.fecha = fecha;
            this.totalFactura = totalFactura;
            this.totalIva = totalIva;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
            this.idCliente = idCliente;
        }
    }
}
