---CREACION BASE DE DATOS ORIGEN 
--Nota: Las tablas de la base de datos destino son iguales a la de origen, sin embargo la base de datos destino no deberia usar identity en ninguna de sus tablas., ya que id se lo pasaremos con c# al igual que el resto de columnas. 
--Nombre: Victor Alfredo Matzar Say
--ID: 000085518
--Correo: 201901473@upana.edu.gt

use UPANA; -- NOMBRE DE SU BASE DE DATOS

CREATE TABLE Pais(
   idPais int not null identity(1,1),
   nombrePais varchar(50) NOT NULL,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE
CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[idPais] ASC
)) ON [PRIMARY]


--DECLARE @SECUENCIA INT
--DECLARE @NOMBRE VARCHAR(50)
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE
--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA < 100)
--BEGIN
--	SET @NOMBRE = 'PAIS_' + CAST(@SECUENCIA AS VARCHAR)

--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2015-01-01    2015-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2015-01-01', '2015-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2015-01-01'))

--	INSERT INTO Pais
--	(nombrePais, estadoRegistro, fechaActualizacion)
--	VALUES(@NOMBRE, @ESTADO, @FECHA)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM Pais;



CREATE TABLE Marca(
   idMarca int not null identity(1,1),
   descripcion varchar(50) NOT NULL,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE
CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[idMarca] ASC
)) ON [PRIMARY]

--DECLARE @SECUENCIA INT
--DECLARE @MARCA VARCHAR(50)
--DECLARE @DESCRIPCION VARCHAR(50)
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE

--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 100)
--BEGIN
--	SET @MARCA = 'MARCA_' + CAST(@SECUENCIA AS VARCHAR)
--	SET @DESCRIPCION = 'DESCRIPCION_' + CAST(@SECUENCIA AS VARCHAR)
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2015-01-01    2015-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2021-01-01', '2021-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2021-01-01'))

--	INSERT INTO Marca
--	(descripcion, estadoRegistro, fechaActualizacion)
--	VALUES(@DESCRIPCION, @ESTADO, @FECHA)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM Marca;


CREATE TABLE Cliente(
   idCliente int not null identity(1,1),
   nombreCliente varchar(50) NOT NULL,
   apellidoCliente varchar(50) NOT NULL,
   nit int not null,
   estadoRegistro varchar(50) not null,
   fechaActualizacion DATE
CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)) ON [PRIMARY]

--DECLARE @SECUENCIA INT
--DECLARE @NOMBRE VARCHAR(50)
--DECLARE @APELLIDO VARCHAR(50)
--DECLARE @NIT INT
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE

--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 1000000)
--BEGIN
--	SET @NOMBRE = 'NOMBRE_' + CAST(@SECUENCIA AS VARCHAR)
--	SET @APELLIDO = 'APELLIDO_' + CAST(@SECUENCIA AS VARCHAR)
--	SET @NIT = FLOOR(RAND()*(999999999-100000000+1))+100000000;
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2020-01-01    2020-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2020-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2020-01-01'))

--	INSERT INTO Cliente
--	(nombreCliente, apellidoCliente, nit, estadoRegistro, fechaActualizacion)
--	VALUES(@NOMBRE, @APELLIDO, @NIT, @ESTADO, @FECHA)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM Cliente;




CREATE TABLE Factura(
   idFactura int not null identity(1,1),
   fecha DATE NOT NULL,
   totalFactura int NOT NULL,
   totalIva int NOT NULL,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE,
   idCliente int not null
CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)) ON [PRIMARY]

--DECLARE @SECUENCIA INT
--DECLARE @FECHAFACTURA DATE
--DECLARE @TOTALFACTURA INT
--DECLARE @TOTALIVA INT
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE
--DECLARE @IDCLIENTE INT

--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 10000)
--BEGIN
--    SET @FECHAFACTURA = DATEADD(day, ROUND(DATEDIFF(day, '2020-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2020-01-01'))
	
--	SET @TOTALFACTURA  = @SECUENCIA + FLOOR(RAND()*(300-100+1))+100;
--	SET @TOTALIVA = @TOTALFACTURA - FLOOR(RAND()*(@SECUENCIA-0+1))+0;
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2020-01-01    2020-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2020-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2020-01-01'))

--	SET @IDCLIENTE  = FLOOR(RAND()*(50-2+1))+2;

--	INSERT INTO Factura
--	(fecha, totalFactura, totalIva, estadoRegistro, fechaActualizacion, idCliente)
--	VALUES(@FECHAFACTURA, @TOTALFACTURA, @TOTALIVA, @ESTADO, @FECHA, @IDCLIENTE)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM Factura;




CREATE TABLE DireccionCliente(
   idDireccion int not null identity(1,1),
   direccion varchar (100) not null,
   cui int NOT NULL,
   telefono int NOT NULL,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE,
   idCliente int not null,
   idPais int not null
CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
(
	[idDireccion] ASC
)) ON [PRIMARY]

--DECLARE @SECUENCIA INT
--DECLARE @DIRECCION VARCHAR(50)
--DECLARE @CUI INT
--DECLARE @TELEFONO INT
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE
--DECLARE @IDCLIENTE INT
--DECLARE @IDPAIS INT
--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 10000)
--BEGIN
--	SET @DIRECCION  = 'DIRECCION_' + CAST(@SECUENCIA AS VARCHAR);
--	SET @CUI = FLOOR(RAND()*(999999999-100000000+1))+100000000;
--	SET @TELEFONO = FLOOR(RAND()*(99999999-11111111+1))+11111111;
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2020-01-01    2020-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2020-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2020-01-01'))

--	SET @IDCLIENTE  = FLOOR(RAND()*(999999-10+1))+10;

--    SET @IDPAIS  = FLOOR(RAND()*(99-2+1))+2;

--	INSERT INTO DireccionCliente
--	(direccion, cui, telefono, estadoRegistro, fechaActualizacion, idCliente, idPais)
--	VALUES(@DIRECCION, @CUI, @TELEFONO, @ESTADO, @FECHA, @IDCLIENTE, @IDPAIS)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM DireccionCliente;


CREATE TABLE Producto(
   idProducto int not null identity(1,1),
   nombreProducto varchar (50) not null,
   precio int NOT NULL,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE,
   idMarca int not null
CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)) ON [PRIMARY]


--DECLARE @SECUENCIA INT
--DECLARE @NOMBRE VARCHAR(50)
--DECLARE @PRECIO INT
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE
--DECLARE @IDMARCA INT
--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 1000)
--BEGIN
--	SET @NOMBRE = 'PRODUCTO_' + CAST(@SECUENCIA AS VARCHAR);
--	SET @PRECIO = FLOOR(RAND()*(300-75+1))+75;
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2020-01-01    2020-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2020-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2020-01-01'))

--    SET @IDMARCA  = FLOOR(RAND()*(99-2+1))+2;

--	INSERT INTO Producto
--	(nombreProducto, precio, estadoRegistro, fechaActualizacion, idMarca)
--	VALUES(@NOMBRE, @PRECIO, @ESTADO, @FECHA, @IDMARCA)
--	SET @SECUENCIA = @SECUENCIA + 1;

SELECT * FROM Producto;



CREATE TABLE FacturaDetalle(
   idDetalle int not null identity(1,1),
   precioUnitario int not null,
   cantidad int not null,
   totalLinea int not null,
   totalIva int not null,
   estadoRegistro varchar(50) NOT NULL,
   fechaActualizacion DATE,
   idFactura int not null,
   idProducto int not null
CONSTRAINT [PK_FacturaDetalle] PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)) ON [PRIMARY]

--DECLARE @SECUENCIA INT
--DECLARE @PRECIOUNITARIO INT
--DECLARE @CANTIDAD INT
--DECLARE @TOTALLINEA INT
--DECLARE @TOTALIVA INT
--DECLARE @ESTADO VARCHAR(50)
--DECLARE @FECHA DATE
--DECLARE @IDFACTURA INT
--DECLARE @IDPRODUCTO INT

--SET @SECUENCIA = 1;
--WHILE(@SECUENCIA <= 1000000)
--BEGIN
--	SET @PRECIOUNITARIO = FLOOR(RAND()*(300-75+1))+75;
--	SET @CANTIDAD = FLOOR(RAND()*(100-2+1))+2;
--	SET @TOTALLINEA = @CANTIDAD * @PRECIOUNITARIO; 
--	SET @TOTALIVA = @TOTALLINEA - @CANTIDAD - @PRECIOUNITARIO;
--	--Variar Estado entre activo e Inactivo. 
--	IF (@SECUENCIA % 2) = 0
--	BEGIN
--		SET @ESTADO = 'ACTIVO';
--	END
--	ELSE
--	BEGIN
--		SET @ESTADO = 'INACTIVO';
--	END
--	--Fechas aleatorias:                     Rango: 2020-01-01    2020-12-31
--	SET @FECHA = DATEADD(day, ROUND(DATEDIFF(day, '2019-01-01', '2020-12-31') * RAND(CHECKSUM(NEWID())), 0),
--    DATEADD(second, CHECKSUM(NEWID()) % 48000, '2019-01-01'))

--    SET @IDFACTURA = FLOOR(RAND()*(999-2+1))+2;
--	SET @IDPRODUCTO = FLOOR(RAND()*(800-20+1))+20;

--	INSERT INTO FacturaDetalle
--	(precioUnitario, cantidad, totalLinea, totalIva, estadoRegistro, fechaActualizacion, idFactura, idProducto)
--	VALUES(@PRECIOUNITARIO, @CANTIDAD, @TOTALLINEA, @TOTALIVA, @ESTADO, @FECHA, @IDFACTURA, @IDPRODUCTO)
--	SET @SECUENCIA = @SECUENCIA + 1;
--END

SELECT * FROM FacturaDetalle;

-- AGREGAR LLAVES FORANEAS. 
  alter table FacturaDetalle
  add constraint FK_Factura_facturaDetalle
  foreign key (idFactura) references Factura(idFactura),
  constraint FK_Producto_facturaDetalle
  foreign key (idProducto) references Producto(idProducto);

  alter table Factura
  add constraint FK_Cliente_Factura
  foreign key (idCliente) references Cliente(idCliente);

  alter table DireccionCliente
  add constraint FK_Cliente_DireccionCliente
  foreign key (idCliente) references Cliente(idCliente),
  constraint FK_Pais_DireccionCliente
  foreign key (idPais) references Pais(idPais);

  alter table Producto
  add constraint FK_Marca_Producto
  foreign key (idMarca) references Marca(idMarca);


-- ELIMINAR LLAVES FORANEAS. 
  alter table FacturaDetalle
  drop constraint FK_Factura_facturaDetalle,
  constraint FK_Producto_facturaDetalle;

  alter table Factura
  drop constraint FK_Cliente_Factura;

  alter table DireccionCliente
  drop constraint FK_Cliente_DireccionCliente,
  constraint FK_Pais_DireccionCliente;

  alter table Producto
  drop constraint FK_Marca_Producto;


-- VALIDACION: ELIMINAR LLAVES FORRANEAS EN TODAS LAS TABLAS
while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
begin
declare @sql nvarchar(2000)
SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
+ '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
FROM information_schema.table_constraints
WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
exec (@sql)
end