using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace EjemplosTasks
{
    class CLProductosInsercionMasiva
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int precio { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idMarca { get; set; }
        //Método 
        public void insertMassiveData(IEnumerable<CLProductosInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idProducto", typeof(int));
            table.Columns.Add("nombreProducto", typeof(string));
            table.Columns.Add("precio", typeof(int));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            table.Columns.Add("idMarca", typeof(int));
            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idProducto,
                        itemDetail.nombreProducto,
                        itemDetail.precio,
                        itemDetail.estadoRegistro,
                        itemDetail.fechaActualizacion,
                        itemDetail.idMarca
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
                            bulkCopy.DestinationTableName = "Producto";
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
