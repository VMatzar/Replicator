using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Replicador
{
    public class CLFacturaInsercionMasiva
    {
        public int idFactura { get; set; }
        public DateTime fecha { get; set; }
        public int totalFactura { get; set; }
        public int totalIva { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idCliente { get; set; }
        public void insertMassiveData(IEnumerable<CLFacturaInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idFactura", typeof(int));
            table.Columns.Add("fecha", typeof(DateTime));
            table.Columns.Add("totalFactura", typeof(int));
            table.Columns.Add("totalIva", typeof(int));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            table.Columns.Add("idCliente", typeof(int));

            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idFactura,
                        itemDetail.fecha,
                        itemDetail.totalFactura,
                        itemDetail.totalIva,
                        itemDetail.estadoRegistro,
                        itemDetail.fechaActualizacion,
                        itemDetail.idCliente
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
                            bulkCopy.DestinationTableName = "Factura";
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
