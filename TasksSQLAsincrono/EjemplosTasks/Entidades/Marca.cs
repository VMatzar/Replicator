using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class Marca
    {
        int idMarca { get; set; }
        string descripcion { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }
        public Marca(int idMarca, string descripcion, string estadoRegistro, DateTime fechaActualizacion)
        {
            this.idMarca = idMarca;
            this.descripcion = descripcion;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
        }
    }
}
