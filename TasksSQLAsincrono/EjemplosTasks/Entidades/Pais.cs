using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemplosTasks
{
    class Pais
    {
        int idPais { get; set; }
        string nombrePais { get; set; }

        string estadoRegistro { get; set; }

        DateTime fechaActualizacion { get; set; }

        public Pais(int idPais, string nombrePais, string estadoRegistro, DateTime fechaActualizacion)
        {
            this.idPais = idPais;
            this.nombrePais = nombrePais;
            this.estadoRegistro = estadoRegistro;
            this.fechaActualizacion = fechaActualizacion;
        }

    }
    }
