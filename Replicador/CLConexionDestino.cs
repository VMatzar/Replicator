using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace Replicador
{
    public class CLConexionDestino
    {
        public static SqlConnection getConnection()
        {
            // Conexion
            ConnectionStringSettings conSettings = ConfigurationManager.ConnectionStrings["MiBasedeDatosDestino"];
            string connectionString = conSettings.ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
