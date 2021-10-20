using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class DireccionCliente
    {
        int idDireccion { get; set; }
        string direccion { get; set; }
        int cui { get; set; }
        int telefono { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }
        int idCliente { get; set; }
        int idPais { get; set; }

        public DireccionCliente(int idDireccion, string direccion, int cui,
            int telefono, string estadoRegistro, DateTime fechaActualizacion, int idCliente, int idPais)
        {
            this.idDireccion = idDireccion;
            this.direccion = direccion;
            this.cui = cui;
            this.telefono = telefono;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
            this.idCliente = idCliente;
            this.idPais = idPais;
        }
    }
}
