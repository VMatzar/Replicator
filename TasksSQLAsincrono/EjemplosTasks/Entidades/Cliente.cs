using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class Cliente
    {
        int idCliente { get; set; }
        string nombreCliente { get; set; }
        string apellidoCliente { get; set; }
        int nit { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }

        public Cliente(int idCliente, string nombreCliente, string apellidoCliente, int nit, string estadoRegistro, DateTime fechaActualizacion)
        {
            this.idCliente = idCliente;
            this.nombreCliente = nombreCliente;
            this.apellidoCliente = apellidoCliente;
            this.nit = nit;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
        }
    }
}
