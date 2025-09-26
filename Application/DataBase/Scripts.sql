CREATE DATABASE BDEtzel;
GO

USE BDEtzel;
GO

---CREACION DE ESQUEMAS----
CREATE SCHEMA CATALOGOS;
CREATE SCHEMA SEGURIDAD;
CREATE SCHEMA PROCESOSPRODUCTIVOS;
CREATE SCHEMA COSTOS;
CREATE SCHEMA LOCALIZACION;
CREATE SCHEMA RECURSOS;
CREATE SCHEMA TRANSACCIONES;
GO


---CREACION DE TABLAS DE LOCALIZACION DEPARTAMENTAL Y MUNICIPAL----
CREATE TABLE LOCALIZACION.Departamento(
	IdDepartamento int primary key identity(1,1),
	NombreDepartamento varchar(100) not null,
);
GO

CREATE TABLE LOCALIZACION.Municipio(
	IdMunicipio int primary key identity(1,1),
	CodigoPostal varchar(100)not null,
	NombreMunicipio varchar(100) not null,
	Id_departamento int references LOCALIZACION.Departamento(IdDepartamento)
);
GO



---CREACION DE TABLAS CATALOGOS----

CREATE TABLE CATALOGOS.TipoSectorEconomico(
	IdTipoSectorEconomico int primary key identity(1,1),
	NombreSector varchar(100) not null,
	DescripcionSector text not null,
);
GO



CREATE TABLE CATALOGOS.EstadoRecurso(
	IdEstadoRecurso int primary key identity(1,1),
	DescripcionEstadoRecurso varchar(100)
);
GO

CREATE TABLE CATALOGOS.TipoRecurso(
	IdTipoRecurso int primary key identity(1,1),
	NombreTipoRecurso varchar(100)

);
GO


----CREACION DE TABLAS DE USURARIO Y ROLES---
CREATE TABLE SEGURIDAD.RolUsuario(
	IdRolUsuario int primary key identity(1,1),
	DescripcionRol varchar(100) not null
);
GO


CREATE TABLE SEGURIDAD.Usuario(
	IdUsuario int primary key identity(1,1),
	Correo varchar(100) not null,
	Clave_hash varchar(255)not null,
	Restablecer bit default 1,
	idTipoUsuario int references SEGURIDAD.RolUsuario(IdRolUsuario),
	idMunicipio int references LOCALIZACION.Municipio(IdMunicipio),
	FechaRegistro datetime default getDate()
);
GO


---CREACION DE TABLAS PARA EL MARKETPLACE-----
CREATE TABLE RECURSOS.RecursosMarketplace(
	IdRecurso int primary key identity(1,1),
	TituloRecurso varchar(100) not null,
	DescripcionRecurso text not null,
	RutaArchivoRecurso varchar(100)null,
	NombreArchivoRecurso varchar(100)null,
	Id_tipoSectorEconomico int references CATALOGOS.TipoSectorEconomico(IdTipoSectorEconomico) not null,
	Id_tipoRecurso int references CATALOGOS.TipoRecurso(IdTipoRecurso)not null,
	Id_estadoRecurso int references CATALOGOS.EstadoRecurso(IdEstadoRecurso)not null,
	Precio decimal(18,2) default 0
);
GO


CREATE TABLE RECURSOS.UsuarioRecursosMarketplace(
	IdRecursoUsuario int primary key identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_recurso int references RECURSOS.RecursosMarketplace(IdRecurso)not null,
	FechaPublicacion datetime default getDate()
);
GO


----CREACION DE TABLAS DE TRANSACCIONES DEL MARKETPLACE-----
CREATE TABLE TRANSACCIONES.DescargaRecursos(
	IdDescarga int primary key identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_recurso int references RECURSOS.RecursosMarketplace(IdRecurso)not null,
	FechaDescarga datetime default getDate()
);
GO













































----CREACION DE TABLAS DE PROCESOS PRODUCTIVOS

CREATE TABLE PROCESOSPRODUCTIVOS.UnidadMedida(
	IdUnidadMedida int primary key identity(1,1),
	DescripcionUnidadMedida varchar(100)not null,

);
GO


CREATE TABLE PROCESOSPRODUCTIVOS.TipoInsumo(
	IdInsumo int primary key identity(1,1),
	NombreInsumo varchar(100),
	FotoInsumo varchar(100)
);
GO


CREATE TABLE PROCESOSPRODUCTIVOS.InsumoUsuario(
	IdInsumo_Usuario int primary key identity(1,1),
	Id_insumo int references CATALOGOS.TipoInsumo(IdInsumo)not null,
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	FechaRegistro datetime default getDate()
);
GO

CREATE TABLE PROCESOSPRODUCTIVOS.Producto(
	IdProducto int primary key identity(1,1),
	NombreProducto varchar(100)not null,
	DescripcionProducto text not null,
	RutaFoto varchar(100)null,
	NombreFoto varchar(100)null,
	Id_sectorEconomico int references CATALOGOS.TipoSectorEconomico(IdTipoSectorEconomico)not null,
	Dimensiones varchar(100)not null,
);
GO



CREATE TABLE PROCESOSPRODUCTIVOS.Producto_Usuario(
	IdProductoUsuario int identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_producto int references PROCESOSPRODUCTIVOS.Producto(IdProducto)not null
);
GO



-----CREACION DE TABLAS DE FORMULACION----
CREATE TABLE PROCESOSPRODUCTIVOS.Formulacion(
	IdFormulacion int primary key identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_producto int references PROCESOSPRODUCTIVOS.Producto(IdProducto)not null,
	NombreFormulacion varchar(100)not null,
	FechaRegistro datetime default getDate()
);
GO

CREATE TABLE PROCESOSPRODUCTIVOS.Detalle_Formulacion(
	idDetalle_Formulacion int primary key identity(1,1),
	id_formulacion int references PROCESOSPRODUCTIVOS.Formulacion(IdFormulacion)not null,
	id_tipoInsumo int references CATALOGOS.TipoInsumo(IdInsumo)not null,
	cantidad decimal(18,2)not null,
	id_unidadMedida int references CATALOGOS.UnidadMedida(IdUnidadMedida),
	fechaRegistro datetime default getDate()
);
GO




















