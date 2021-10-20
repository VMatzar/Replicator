using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Replicador
{
    public class CLFacturaDetalleIncercionMasiva
    {
        //attributes
        public int idDetalle { get; set; }
        public int precioUnitario { get; set; }
        public int cantidad { get; set; }
        public int totalLinea { get; set; }
        public int totalIva { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int idFactura { get; set; }
        public int idProducto { get; set; }
        //methods 
        public void insertMassiveData(IEnumerable<CLFacturaDetalleIncercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idDetalle", typeof(int));
            table.Columns.Add("precioUnitario", typeof(int));
            table.Columns.Add("cantidad", typeof(int));
            table.Columns.Add("totalLinea", typeof(int));
            table.Columns.Add("totalIva", typeof(int));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            table.Columns.Add("idFactura", typeof(int));
            table.Columns.Add("idProducto", typeof(int));

            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idDetalle,
                        itemDetail.precioUnitario,
                        itemDetail.cantidad,
                        itemDetail.totalLinea,
                        itemDetail.totalIva,
                        itemDetail.estadoRegistro,
                        itemDetail.fechaActualizacion,
                        itemDetail.idFactura,
                        itemDetail.idProducto
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
                            bulkCopy.DestinationTableName = "FacturaDetalle";
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
