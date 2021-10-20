using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace EjemplosTasks
{
    class Proceso
    {
        public void tarea01()
        {
            Console.WriteLine("Tarea01");

            deleteForeignKeys();

            var listaPaises = ListaPaisAsync();
            var listaClientes = ListaClienteAsync();
            var listaDireccionClientes = ListaDireccionClienteAsync();
            var listaFacturas = ListaFacturaAsync();
            var listaFacturaDetalles = ListaFacturaDetalleAsync();
            var listaMarcas = ListaMarcaAsync();
            var listaProductos = ListaProductoAsync();

            listaPaises.Wait();
            listaClientes.Wait();
            listaDireccionClientes.Wait();
            listaFacturas.Wait();
            listaFacturaDetalles.Wait();
            listaMarcas.Wait();
            listaProductos.Wait();

            addForeignKeys();

            Console.ReadKey();
        }
        public void deleteForeignKeys()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
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
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE Pais";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idPais, nombrePais, estadoRegistro, fechaActualizacion " +
                    " FROM Pais " +
                    " ORDER BY idPais ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader paisesReader = await cmd.ExecuteReaderAsync();
            List<CLPaisInsercionMasiva> objectDetailList = new List<CLPaisInsercionMasiva>();

            Console.WriteLine("Async Listado de Paises Data to Console:");
            while (paisesReader.Read())
            {
                Console.WriteLine("Pais id {0}, nombre {1}, estado {2}, fecha{3}", paisesReader[0], paisesReader[1], paisesReader[2], paisesReader[3]);
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
            return objectDetailList;
        }
        public async Task<List<CLClienteInsercionMasiva>> ListaClienteAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE Cliente";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idCliente, nombreCliente, apellidoCliente, nit, estadoRegistro, fechaActualizacion " +
                    " FROM Cliente " +
                    " ORDER BY idCliente ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader clienteReader = await cmd.ExecuteReaderAsync();
            List<CLClienteInsercionMasiva> objectDetailList = new List<CLClienteInsercionMasiva>();

            Console.WriteLine("Async Listado de Cliente:");
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
            return objectDetailList;
        }
        public async Task<List<CLDireccionClienteInsercionMasiva>> ListaDireccionClienteAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE DireccionCliente";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idDireccion, direccion, cui, telefono, estadoRegistro, fechaActualizacion, idCliente, idPais " +
                    " FROM DireccionCliente " +
                    " ORDER BY idDireccion ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader direccionClienteReader = await cmd.ExecuteReaderAsync();
            List<CLDireccionClienteInsercionMasiva> objectDetailList = new List<CLDireccionClienteInsercionMasiva>();

            Console.WriteLine("Async Listado de direccionCliente:");
            while (direccionClienteReader.Read())
            {
                var detail = new CLDireccionClienteInsercionMasiva()
                {
                        idDireccion= Convert.ToInt32(direccionClienteReader[0]),
                        direccion= Convert.ToString(direccionClienteReader[1]),
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
            return objectDetailList;
        }
        public async Task<List<CLMarcaInsercionMasiva>> ListaMarcaAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE Marca";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idMarca, descripcion, estadoRegistro, fechaActualizacion " +
                    " FROM Marca " +
                    " ORDER BY idMarca ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader marcaReader = await cmd.ExecuteReaderAsync();
            List<CLMarcaInsercionMasiva> objectDetailList = new List<CLMarcaInsercionMasiva>();

            Console.WriteLine("Async Listado de Marcas:");
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
            return objectDetailList;
        }
        public async Task<List<CLFacturaInsercionMasiva>> ListaFacturaAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE Factura";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idFactura, fecha, totalFactura, totalIva, estadoRegistro, fechaActualizacion, idCliente " +
                    " FROM Factura " +
                    " ORDER BY idFactura ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader facturaReader = await cmd.ExecuteReaderAsync();
            List<CLFacturaInsercionMasiva> objectDetailList = new List<CLFacturaInsercionMasiva>();

            Console.WriteLine("Async Listado de Facturas:");
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
            return objectDetailList;
        }
        public async Task<List<CLFacturaDetalleIncercionMasiva>> ListaFacturaDetalleAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE FacturaDetalle";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idDetalle, precioUnitario, cantidad, totalLinea, totalIva, estadoRegistro, fechaActualizacion, idFactura, idProducto " +
                    " FROM FacturaDetalle " +
                    " ORDER BY idDetalle ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader facturaDetalleReader = await cmd.ExecuteReaderAsync();
            List<CLFacturaDetalleIncercionMasiva> objectDetailList = new List<CLFacturaDetalleIncercionMasiva>();

            Console.WriteLine("Async Listado de Factura Detalles:");
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
            return objectDetailList;
        }
        public async Task<List<CLProductosInsercionMasiva>> ListaProductoAsync()
        {
            using (var connection = CLConexionDestino.getConnection())
            {
                connection.Open();
                string query = "TRUNCATE TABLE Producto";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }

            string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
            SqlConnection con = new SqlConnection(connString);
            await con.OpenAsync();
            string instruccionSql = " SELECT  idProducto, nombreProducto, precio, estadoRegistro, fechaActualizacion, idMarca " +
                    " FROM Producto " +
                    " ORDER BY idProducto ";

            SqlCommand cmd = new SqlCommand(instruccionSql, con);
            SqlDataReader productoReader = await cmd.ExecuteReaderAsync();
            List<CLProductosInsercionMasiva> objectDetailList = new List<CLProductosInsercionMasiva>();

            Console.WriteLine("Async Listado de Productos:");
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

        //public async Task<List<Cliente>> ListaClienteAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idCliente, nombreCliente, apellidoCliente, nit, estadoRegistro, fechaActualizacion " +
        //            " FROM Cliente " +
        //            " ORDER BY idCliente ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<Cliente> listaClientes = new List<Cliente>();
        //    Console.WriteLine("\nAsync Clientes");
        //    while (reader.Read())
        //    {
        //        listaClientes.Add(new Cliente(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToString(reader[1]),
        //                Convert.ToString(reader[2]),
        //                Convert.ToInt32(reader[3]),
        //                Convert.ToString(reader[4]),
        //                Convert.ToDateTime(reader[5])
        //            ));
        //        Console.WriteLine("Async Cliente id: {0} , nombre: {1}, apellido: {2}, nit: {3}, Estado: {4}, Fecha: {5} ", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
        //    }
        //    con.Close();

        //    return listaClientes;
        //}


        //public async Task<List<DireccionCliente>> ListaDireccionClienteAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idDireccion, direccion, cui, telefono, estadoRegistro, fechaActualizacion, idCliente, idPais " +
        //            " FROM DireccionCliente " +
        //            " ORDER BY idDireccion ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<DireccionCliente> listaDireccionClientes = new List<DireccionCliente>();
        //    Console.WriteLine("\nAsync Direcciones");
        //    while (reader.Read())
        //    {
        //        listaDireccionClientes.Add(new DireccionCliente(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToString(reader[1]),
        //                Convert.ToInt32(reader[2]),
        //                Convert.ToInt32(reader[3]),
        //                Convert.ToString(reader[4]),
        //                Convert.ToDateTime(reader[5]),
        //                Convert.ToInt32(reader[6]),
        //                Convert.ToInt32(reader[7])
        //            ));
        //        Console.WriteLine("Async Direccion id: {0} , direccion: {1}, Cui: {2}, telefono: {3}, Estado: {4}, Fecha: {5}, idCliente: {6}, idPais {7}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]);
        //    }
        //    con.Close();

        //    return listaDireccionClientes;
        //}
        //public async Task<List<Factura>> ListaFacturaAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idFactura, fecha, totalFactura, totalIva, estadoRegistro, fechaActualizacion, idCliente " +
        //            " FROM Factura " +
        //            " ORDER BY idFactura ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<Factura> listaFacturas = new List<Factura>();
        //    Console.WriteLine("\nAsync Facturas");
        //    while (reader.Read())
        //    {
        //        listaFacturas.Add(new Factura(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToDateTime(reader[1]),
        //                Convert.ToInt32(reader[2]),
        //                Convert.ToInt32(reader[3]),
        //                Convert.ToString(reader[4]),
        //                Convert.ToDateTime(reader[5]),
        //                Convert.ToInt32(reader[6])
        //            ));
        //        Console.WriteLine("Async Factura id: {0} , fecha: {1}, totalFactura: {2}, total Iva: {3}, Estado: {4}, Fecha: {5}, idCliente: {6}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
        //    }
        //    con.Close();

        //    return listaFacturas;
        //}
        //public async Task<List<FacturaDetalle>> ListaFacturaDetalleAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idDetalle, precioUnitario, cantidad, totalLinea, totalIva, estadoRegistro, fechaActualizacion, idFactura, idProducto " +
        //            " FROM FacturaDetalle " +
        //            " ORDER BY idDetalle ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<FacturaDetalle> listaDetalleFacturas = new List<FacturaDetalle>();
        //    Console.WriteLine("\nAsync Factura Detalle");
        //    while (reader.Read())
        //    {
        //        listaDetalleFacturas.Add(new FacturaDetalle(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToInt32(reader[1]),
        //                Convert.ToInt32(reader[2]),
        //                Convert.ToInt32(reader[3]),
        //                Convert.ToInt32(reader[4]),
        //                Convert.ToString(reader[5]),
        //                Convert.ToDateTime(reader[6]),
        //                Convert.ToInt32(reader[7]),
        //                Convert.ToInt32(reader[8])
        //            ));
        //        Console.WriteLine("Async Factura Detalle id: {0} , precio unitario: {1}, cantidad: {2},  total linea: {3}, total Iva: {4}, Estado: {5}, Fecha: {6}, idFactura: {7},  idProducto: {8},", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]);
        //    }
        //    con.Close();

        //    return listaDetalleFacturas;
        //}
        //public async Task<List<Marca>> ListaMarcaAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idMarca, descripcion, estadoRegistro, fechaActualizacion " +
        //            " FROM Marca " +
        //            " ORDER BY idMarca ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<Marca> listaMarcas = new List<Marca>();
        //    Console.WriteLine("\nAsync Marcas");
        //    while (reader.Read())
        //    {
        //        listaMarcas.Add(new Marca(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToString(reader[1]),
        //                Convert.ToString(reader[2]),
        //                Convert.ToDateTime(reader[3])
        //            ));
        //        Console.WriteLine("Async Marca id: {0} , descripcion: {1}, estado Registro : {2},  fecha: {3}", reader[0], reader[1], reader[2], reader[3]);
        //    }
        //    con.Close();

        //    return listaMarcas;
        //}
        //public async Task<List<Producto>> ListaProductoAsync()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["MiBasedeDatos"].ToString();
        //    SqlConnection con = new SqlConnection(connString);
        //    await con.OpenAsync();
        //    string instruccionSQL = " SELECT  idProducto, nombreProducto, precio, estadoRegistro, fechaActualizacion, idMarca " +
        //            " FROM Producto " +
        //            " ORDER BY idProducto ";
        //    SqlCommand cmd = new SqlCommand(instruccionSQL, con);
        //    SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<Producto> listaProductos = new List<Producto>();
        //    Console.WriteLine("\nAsync Productos");
        //    while (reader.Read())
        //    {
        //        listaProductos.Add(new Producto(
        //                Convert.ToInt32(reader[0]),
        //                Convert.ToString(reader[1]),
        //                Convert.ToInt32(reader[2]),
        //                Convert.ToString(reader[3]),
        //                Convert.ToDateTime(reader[4]),
        //                Convert.ToInt32(reader[5])
        //            ));
        //        Console.WriteLine("Async Producto id: {0} , nombre: {1}, precio: {2}, estado: {3}, fecha: {4}, idMarca: {5}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
        //    }
        //    con.Close();

        //    return listaProductos;
        //}
    }
}
