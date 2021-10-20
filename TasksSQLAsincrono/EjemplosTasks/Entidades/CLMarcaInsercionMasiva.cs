using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace EjemplosTasks
{
    class CLMarcaInsercionMasiva
    {
        //attributes
        public int idMarca { get; set; }
        public string descripcion { get; set; }
        public string estadoRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        //methods 
        public void insertMassiveData(IEnumerable<CLMarcaInsercionMasiva> detailsList)
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("idMarca", typeof(int));
            table.Columns.Add("descripcion", typeof(string));
            table.Columns.Add("estadoRegistro", typeof(string));
            table.Columns.Add("fechaActualizacion", typeof(DateTime));

            foreach (var itemDetail in detailsList)
            {
                table.Rows.Add(new object[]
                    {
                        itemDetail.idMarca,
                        itemDetail.descripcion,
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
                            bulkCopy.DestinationTableName = "Marca";
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
