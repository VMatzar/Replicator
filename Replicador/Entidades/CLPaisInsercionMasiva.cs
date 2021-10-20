using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Replicador
{
    public class CLPaisInsercionMasiva
    {
        //attributes
        public int idPais { get; set; }
        public string nombrePais { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        //methods
        public void insertMassiveData(IEnumerable<CLPaisInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idPais", typeof(int));
            table.Columns.Add("nombrePais", typeof(string));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));
            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idPais,
                        itemDetail.nombrePais ,
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
                            bulkCopy.DestinationTableName = "Pais";
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
