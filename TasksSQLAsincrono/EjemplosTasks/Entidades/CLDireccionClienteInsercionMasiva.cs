using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace EjemplosTasks
{
    class CLDireccionClienteInsercionMasiva
    {
        //attributes
        public int idDireccion { get; set; }
        public string direccion { get; set; }
        public int cui { get; set; }
        public int telefono { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idCliente { get; set; }
        public int idPais { get; set; }
        //methods 
        public void insertMassiveData(IEnumerable<CLDireccionClienteInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idDireccion", typeof(int));
            table.Columns.Add("direccion", typeof(string));
            table.Columns.Add("cui", typeof(int));
            table.Columns.Add("telefono", typeof(int));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            table.Columns.Add("idCliente", typeof(int));
            table.Columns.Add("idPais", typeof(int));

            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idDireccion,
                        itemDetail.direccion,
                        itemDetail.cui,
                        itemDetail.telefono,
                        itemDetail.estadoRegistro,
                        itemDetail.fechaActualizacion,
                        itemDetail.idCliente,
                        itemDetail.idPais
                    });
            }
            //insert to db
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "DireccionCliente";
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            connection.Close();
                            throw;
                        }
                    }
                }
            }
        }
    }
}
