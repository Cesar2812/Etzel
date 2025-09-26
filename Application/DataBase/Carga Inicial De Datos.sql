                                                ---CARGA INICIAL DE DATOS---
                                                

---ESQUEMA DE LOCALIZACION---
---TABLA DEPARTAMENTO----
INSERT INTO LOCALIZACION.Departamento(NombreDepartamento)
VALUES('Granada'),('Masaya'),('Managua'),('Carazo'),('Rivas'),('Leon'),('Chinandega')
GO


---TABLA MUNICIPIO---
---GRANADA---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('43000','Nandaime',1),('43000','Diria',1),('43000','Diriomo',1),('43000','Granada',1)
GO

---MASAYA---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('42500','Catarina',2),('42200','Nindiri',2),('41000','Masaya',2),('42800','San Juan De Oriente',2),
('42100','Tisma',2),('42400','Nandasmo',2),('42300','La Concepcion',2),('42600','Masatepe',2),('42700 ','Niquinohomo',2)
GO

---MANAGUA---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('10000','Managua',3),('15100','Tipitapa',3),('15300','San Francisco Libre',3),('15500','Mateare',3),
('15700','Ciudad Sandino',3),('16100','El Crucero',3),('16300','Ticuantepe',3),('16500','Villa El Carmen',3),
('16700 ','San Rafael Del Sur',3)
GO

----CARAZO---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('45000','Jinotepe',4),('46100','Dolores',4),('46200','El Rosario',4),('46300','Diriamba',4),
('46400','San Marcos',4),('46500','La Paz de Carazo',4),('46600','Santa Teresa',4),('46700','La Conquista',4)
GO

---RIVAS---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('47000','Rivas',5),('48100','San Jorge',5),('48200','Buenos Aires',5),('48300','Potosí',5),
('48400','Belén',5),('48500','Tola',5),('48600','San Juan del Sur',5),('48700','Moyogalpa',5),
('48800','Altagracia',5),('48900','Cárdenas',5)
GO

---LEON---
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('21000','León',6),('22100','La Paz Centro',6),('22200','Nagarote',6),('22300','Quezalguaque',6),
('22400','Telica',6),('22500','Larreynaga',6),('22600','El Jicaral',6),('22700','Santa Rosa del Peñón',6),
('22800','El Sauce',6),('22900','Achuapa',6)
GO


---CHINANDEGA----
INSERT INTO LOCALIZACION.Municipio(CodigoPostal,NombreMunicipio,Id_departamento)
VALUES('25000','Chinandega',7),('26100','Chichigalpa',7),('26200','El Viejo',7),('26300','El Realejo',7),
('26400','Corinto',7),('26500','Posoltega',7),('26600','Puerto Morazán',7),('26700','Villanueva',7),
('26800','Somotillo',7),('26900','San Francisco del Norte',7),
('27100','Santo Tómas del Norte',7),('27200','Cinco Pinos',7),('27300','San Pedro del Norte',7)
GO


----ESQUEMA DE CATALOGOS---

----TABLA TIPOSECTORECONOMICO---
INSERT INTO CATALOGOS.TipoSectorEconomico(NombreSector)
VALUES('Textil y Vestuario'),('Madera y Muebles'),('Cuero y Productos Derivados'),('Alimentos y Bebidas')

---TABLA ESTADO RECURSO---
INSERT INTO CATALOGOS.EstadoRecurso(DescripcionEstadoRecurso)
VALUES('Gratuito'),('De Paga')

---TABLA TIPORECURSO-----
INSERT INTO CATALOGOS.TipoRecurso(NombreTipoRecurso)
VALUES('Patron'),('Molde'),('Plantilla'),('Formulario'),('Manual')



---ESQUEMA DE SEGURIDAD---
----TABLA USUARIO ROL---
INSERT INTO SEGURIDAD.RolUsuario(DescripcionRol)
VALUES('MIPYME'),('Experto'),('Diseñador')
GO
INSERT INTO SEGURIDAD.RolUsuario(DescripcionRol)
VALUES('Artesano')
GO

Select * from SEGURIDAD.RolUsuario