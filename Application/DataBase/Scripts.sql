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
CREATE TABLE CATALOGOS.Persona(
	IdPersona int primary key identity(1,1),
	NombrePersona varchar(100)not null,
	Id_municipio int references LOCALIZACION.Municipio(IdMunicipio) not null
);
GO


CREATE TABLE CATALOGOS.Genero(
	IdGenero int primary key identity(1,1),
	DescripcionGenero varchar(100)not null,
);
GO


CREATE TABLE CATALOGOS.PersonaNatural(
	IdPersonaNatural int primary key identity(1,1),
	CedulaPerosonaNatural varchar(100)not null,
	Id_persona int references CATALOGOS.Persona(IdPersona)not null unique,
	ApellidoPersonaNatural varchar(100)not null,
	Id_genero int references CATALOGOS.Genero(IdGenero)not null,
	FechaRegistro datetime default getDate()
);
GO


CREATE TABLE CATALOGOS.PersonaJuridica(
	IdPersonaJuridica int primary key identity(1,1),
	NumeroRuc varchar(100) not null,
	Id_persona int references CATALOGOS.Persona(IdPersona)not null unique,
	RazonSocial varchar(100) not null,
	FechaRegistro datetime default getDate()
);
GO








CREATE TABLE CATALOGOS.TipoSectorEconomico(
	IdTipoSectorEconomico int primary key identity(1,1),
	NombreSector varchar(100) not null,
	DescripcionSector text not null,
);
GO


CREATE TABLE CATALOGOS.TipoPresentacion(
	IdTipoPresentacion int primary key identity(1,1),
	DescripcionPresentacion varchar(100) not null
);
GO


CREATE TABLE CATALOGOS.UnidadMedida(
	IdUnidadMedida int primary key identity(1,1),
	DescripcionUnidadMedida varchar(100)not null,

);
GO

CREATE TABLE CATALOGOS.TipoInsumo(
	IdInsumo int primary key identity(1,1),
	NombreInsumo varchar(100),
	FotoInsumo varchar(100)
);
GO

CREATE TABLE CATALOGOS.InsumoUsuario(
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
	Id_sectorEconomico int references CATALOGOS.TipoSectorEconomico(IdTipoSectorEconomico)not null,
	Dimensiones varchar(100)not null,
);
GO

CREATE TABLE PROCESOSPRODUCTIVOS.ProductoPresentacionUsuario(
	IdProductoPresentacionUsuario int identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_producto int references CATALOGOS.Producto(IdProducto)not null,
	Id_tipoPresentacion int references CATALOGOS.TipoPresentacion(IdTipoPresentacion)not null,
);
GO


CREATE TABLE PROCESOSPRODUCTIVOS.Producto_Usuario(
	IdProductoUsuario int identity(1,1),
	Id_usuario int references SEGURIDAD.Usuario(IdUsuario)not null,
	Id_producto int references CATALOGOS.Producto(IdProducto)not null
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






----CREACION DE TABLAS DE USURARIO Y ROLES---
CREATE TABLE SEGURIDAD.RolUsuario(
	IdRolUsuario int primary key identity(1,1),
	DescripcionRol varchar(100) not null
);
GO


CREATE TABLE SEGURIDAD.Usuario(
	IdUsuario int primary key identity(1,1),
	Foto varchar(100) not null,
	idpersona int references CATALOGOS.Persona(IdPersona)not null unique,
	Correo varchar(100) not null,
	Clave_hash varchar(255)not null,
	Restablecer bit default 1,
	idtipoUsuario int references SEGURIDAD.RolUsuario(IdRolUsuario),
	FechaRegistro datetime default getDate()
);
GO














