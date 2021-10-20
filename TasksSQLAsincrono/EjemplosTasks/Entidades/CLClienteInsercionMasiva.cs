using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace EjemplosTasks
{
    class CLClienteInsercionMasiva
    {
        //attributes
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public int nit { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        //methods 
        public void insertMassiveData(IEnumerable<CLClienteInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idCliente", typeof(int));
            table.Columns.Add("nombreCliente", typeof(string));
            table.Columns.Add("apellidoCliente", typeof(string));
            table.Columns.Add("nit", typeof(int));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idCliente,
                        itemDetail.nombreCliente,
                        itemDetail.apellidoCliente,
                        itemDetail.nit,
                        itemDetail.estadoRegistro,
                        itemDetail.fechaActualizacion
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
                            bulkCopy.DestinationTableName = "Cliente";
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
