using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class Producto
    {
        int idProducto { get; set; }
        string nombreProducto { get; set; }
        int precio { get; set; }
        string estadoRegistro { get; set; }
        DateTime fechaActualizacion { get; set; }
        int idMarca { get; set; }

        public Producto(int idProducto, string nombreProducto, int precio, string estadoRegistro, DateTime fechaActualizacion, int idMarca)
        {
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
            this.precio = precio;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
            this.idMarca = idMarca;
        }
    }
}
