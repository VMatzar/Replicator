using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Replicador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void BTNReplicar_Click(object sender, EventArgs e)
        {
            BTNReplicar.Enabled = false;
            pgProcesamiento.Visible = true;
            timer.Enabled = true;
            
            //Eliminar llaves forraneas.
            deleteForeignKeys();

            //Tareas asincronas. 
            var listaPaises = ListaPaisAsync();
            var listaClientes = ListaClienteAsync();
            var listaDireccionClientes = ListaDireccionClienteAsync();
            var listaFacturas = ListaFacturaAsync();
            var listaFacturaDetalles = ListaFacturaDetalleAsync();
            var listaMarcas = ListaMarcaAsync();
            var listaProductos = ListaProductoAsync();
            await Task.WhenAll(listaPaises, listaClientes, listaDireccionClientes, listaFacturas, listaFacturaDetalles, listaMarcas, listaProductos);

            //Añadir llaves forraneas.
            addForeignKeys();
        }
        public void deleteForeignKeys()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                //La siguiente consulta elimina llaves forraneas en las tablas, si existen!
                string query = "while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY')) " +
                               " begin " +
                               " declare @sql nvarchar(2000) " +
                               " SELECT TOP 1 @sql = ('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME " +
                               " + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']') " +
                               " FROM information_schema.table_constraints " +
                               " WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' " +
                               " exec(@sql) " +
                               " end ";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public async Task<List<CLPaisInsercionMasiva>> ListaPaisAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE Pais";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            
            List<CLPaisInsercionMasiva> objectDetailList = new List<CLPaisInsercionMasiva>();

            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idPais, nombrePais, estadoRegistro, fechaActualizacion " +
                        " FROM Pais " +
                        " ORDER BY idPais ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader paisesReader = cmd.ExecuteReader();
                while (paisesReader.Read())
                {
                    var detail = new CLPaisInsercionMasiva()
                    {
                        idPais = Convert.ToInt32(paisesReader[0]),
                        nombrePais = Convert.ToString(paisesReader[1]),
                        estadoRegistro = Convert.ToString(paisesReader[2]),
                        fechaActualizacion = Convert.ToDateTime(paisesReader[3])
                    };
                    objectDetailList.Add(detail);
                }
                CLPaisInsercionMasiva detailModel = new CLPaisInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            return objectDetailList;
        }
        public async Task<List<CLDireccionClienteInsercionMasiva>> ListaDireccionClienteAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE DireccionCliente";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            List<CLDireccionClienteInsercionMasiva> objectDetailList = new List<CLDireccionClienteInsercionMasiva>();
            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idDireccion, direccion, cui, telefono, estadoRegistro, fechaActualizacion, idCliente, idPais " +
                        " FROM DireccionCliente " +
                        " ORDER BY idDireccion ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader direccionClienteReader = cmd.ExecuteReader();

                while (direccionClienteReader.Read())
                {
                    var detail = new CLDireccionClienteInsercionMasiva()
                    {
                        idDireccion = Convert.ToInt32(direccionClienteReader[0]),
                        direccion = Convert.ToString(direccionClienteReader[1]),
                        cui = Convert.ToInt32(direccionClienteReader[2]),
                        telefono = Convert.ToInt32(direccionClienteReader[3]),
                        estadoRegistro = Convert.ToString(direccionClienteReader[4]),
                        fechaActualizacion = Convert.ToDateTime(direccionClienteReader[5]),
                        idCliente = Convert.ToInt32(direccionClienteReader[6]),
                        idPais = Convert.ToInt32(direccionClienteReader[7])
                    };
                    objectDetailList.Add(detail);
                }
                CLDireccionClienteInsercionMasiva detailModel = new CLDireccionClienteInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            return objectDetailList;
        }
        public async Task<List<CLMarcaInsercionMasiva>> ListaMarcaAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE Marca";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            List<CLMarcaInsercionMasiva> objectDetailList = new List<CLMarcaInsercionMasiva>();

            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idMarca, descripcion, estadoRegistro, fechaActualizacion " +
                        " FROM Marca " +
                        " ORDER BY idMarca ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader marcaReader = cmd.ExecuteReader();

                while (marcaReader.Read())
                {
                    var detail = new CLMarcaInsercionMasiva()
                    {
                        idMarca = Convert.ToInt32(marcaReader[0]),
                        descripcion = Convert.ToString(marcaReader[1]),
                        estadoRegistro = Convert.ToString(marcaReader[2]),
                        fechaActualizacion = Convert.ToDateTime(marcaReader[3])
                    };
                    objectDetailList.Add(detail);
                }
                CLMarcaInsercionMasiva detailModel = new CLMarcaInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            
            return objectDetailList;
        }
        public async Task<List<CLFacturaInsercionMasiva>> ListaFacturaAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE Factura";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            
            List<CLFacturaInsercionMasiva> objectDetailList = new List<CLFacturaInsercionMasiva>();

            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idFactura, fecha, totalFactura, totalIva, estadoRegistro, fechaActualizacion, idCliente " +
                        " FROM Factura " +
                        " ORDER BY idFactura ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader facturaReader = cmd.ExecuteReader();

                while (facturaReader.Read())
                {
                    var detail = new CLFacturaInsercionMasiva()
                    {
                        idFactura = Convert.ToInt32(facturaReader[0]),
                        fecha = Convert.ToDateTime(facturaReader[1]),
                        totalFactura = Convert.ToInt32(facturaReader[2]),
                        totalIva = Convert.ToInt32(facturaReader[3]),
                        estadoRegistro = Convert.ToString(facturaReader[4]),
                        fechaActualizacion = Convert.ToDateTime(facturaReader[5]),
                        idCliente = Convert.ToInt32(facturaReader[6])
                    };
                    objectDetailList.Add(detail);
                }
                CLFacturaInsercionMasiva detailModel = new CLFacturaInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            return objectDetailList;
        }
        public async Task<List<CLFacturaDetalleIncercionMasiva>> ListaFacturaDetalleAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE FacturaDetalle";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            List<CLFacturaDetalleIncercionMasiva> objectDetailList = new List<CLFacturaDetalleIncercionMasiva>();

            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idDetalle, precioUnitario, cantidad, totalLinea, totalIva, estadoRegistro, fechaActualizacion, idFactura, idProducto " +
                        " FROM FacturaDetalle " +
                        " ORDER BY idDetalle ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader facturaDetalleReader = cmd.ExecuteReader();

                while (facturaDetalleReader.Read())
                {
                    var detail = new CLFacturaDetalleIncercionMasiva()
                    {
                        idDetalle = Convert.ToInt32(facturaDetalleReader[0]),
                        precioUnitario = Convert.ToInt32(facturaDetalleReader[1]),
                        cantidad = Convert.ToInt32(facturaDetalleReader[2]),
                        totalLinea = Convert.ToInt32(facturaDetalleReader[3]),
                        totalIva = Convert.ToInt32(facturaDetalleReader[4]),
                        estadoRegistro = Convert.ToString(facturaDetalleReader[5]),
                        fechaActualizacion = Convert.ToDateTime(facturaDetalleReader[6]),
                        idFactura = Convert.ToInt32(facturaDetalleReader[7]),
                        idProducto = Convert.ToInt32(facturaDetalleReader[8])
                    };
                    objectDetailList.Add(detail);
                }
                CLFacturaDetalleIncercionMasiva detailModel = new CLFacturaDetalleIncercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            return objectDetailList;
        }
        public async Task<List<CLProductosInsercionMasiva>> ListaProductoAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE Producto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });
            List<CLProductosInsercionMasiva> objectDetailList = new List<CLProductosInsercionMasiva>();
            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idProducto, nombreProducto, precio, estadoRegistro, fechaActualizacion, idMarca " +
                        " FROM Producto " +
                        " ORDER BY idProducto ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader productoReader = cmd.ExecuteReader();

                while (productoReader.Read())
                {
                    var detail = new CLProductosInsercionMasiva()
                    {
                        idProducto = Convert.ToInt32(productoReader[0]),
                        nombreProducto = Convert.ToString(productoReader[1]),
                        precio = Convert.ToInt32(productoReader[2]),
                        estadoRegistro = Convert.ToString(productoReader[3]),
                        fechaActualizacion = Convert.ToDateTime(productoReader[4]),
                        idMarca = Convert.ToInt32(productoReader[5])
                    };
                    objectDetailList.Add(detail);
                }
                CLProductosInsercionMasiva detailModel = new CLProductosInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });
            return objectDetailList;
        }
        public async Task<List<CLClienteInsercionMasiva>> ListaClienteAsync()
        {
            await Task.Run(() =>
            {
                using (var connection = CLConexionDestino.getConnection())
                {
                    connection.Open();
                    string query = "TRUNCATE TABLE Cliente";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            });

            List<CLClienteInsercionMasiva> objectDetailList = new List<CLClienteInsercionMasiva>();

            await Task.Run(() =>
            {
                string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string instruccionSql = " SELECT  idCliente, nombreCliente, apellidoCliente, nit, estadoRegistro, fechaActualizacion " +
                        " FROM Cliente " +
                        " ORDER BY idCliente ";

                SqlCommand cmd = new SqlCommand(instruccionSql, con);
                SqlDataReader clienteReader = cmd.ExecuteReader();

                while (clienteReader.Read())
                {
                    var detail = new CLClienteInsercionMasiva()
                    {
                        idCliente = Convert.ToInt32(clienteReader[0]),
                        nombreCliente = Convert.ToString(clienteReader[1]),
                        apellidoCliente = Convert.ToString(clienteReader[2]),
                        nit = Convert.ToInt32(clienteReader[3]),
                        estadoRegistro = Convert.ToString(clienteReader[4]),
                        fechaActualizacion = Convert.ToDateTime(clienteReader[5])
                    };
                    objectDetailList.Add(detail);
                }
                CLClienteInsercionMasiva detailModel = new CLClienteInsercionMasiva();
                detailModel.insertMassiveData(objectDetailList);
                con.Close();
            });

            
            return objectDetailList;
        }
        public void addForeignKeys()
        {
            using (var connection1 = CLConexionDestino.getConnection())
            {
                connection1.Open();
                string query = "alter table FacturaDetalle " +
                                " add constraint FK_Factura_facturaDetalle " +
                                " foreign key(idFactura) references Factura(idFactura), " +
                                " constraint FK_Producto_facturaDetalle " +
                                " foreign key(idProducto) references Producto(idProducto); " +
                                " alter table Factura " +
                                " add constraint FK_Cliente_Factura " +
                                " foreign key(idCliente) references Cliente(idCliente); " +
                                " alter table DireccionCliente " +
                                " add constraint FK_Cliente_DireccionCliente " +
                                " foreign key(idCliente) references Cliente(idCliente), " +
                                " constraint FK_Pais_DireccionCliente " +
                                " foreign key(idPais) references Pais(idPais); " +
                                " alter table Producto " +
                                " add constraint FK_Marca_Producto " +
                                " foreign key(idMarca) references Marca(idMarca); ";
                SqlCommand command = new SqlCommand(query, connection1);
                command.ExecuteNonQuery();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if(pgProcesamiento.Value >= 270)
            {
                timer.Stop();
                pgProcesamiento.Value = 0;
                lblCarga.Text = "Replica completada";
                MessageBox.Show("Se han replicado los datos exitosamente");
                BTNReplicar.Enabled = true;
            }
            else
            {
                pgProcesamiento.Value += 1;
                float proceso = (pgProcesamiento.Value * 100) / 270;
                lblCarga.Text = "Please wait ... LOADING " + proceso + "%";
            }
        }
    }
}
