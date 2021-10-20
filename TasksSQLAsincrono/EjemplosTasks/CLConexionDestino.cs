using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace EjemplosTasks
{
    class CLConexionDestino
    {
        //Conexion para mi BulkSqlCopy
        public static SqlConnection getConnection()
        {
            // Conexion
            ConnectionStringSettings conSettings = ConfigurationManager.ConnectionStrings["MiBasedeDatosDestino"];
            string connectionString = conSettings.ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
