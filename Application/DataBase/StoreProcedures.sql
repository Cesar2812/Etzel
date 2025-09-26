
----PROCEDIMIENTOS ALMACENADOS

---PROCEDMIENTO ALMACENADO PARA INSERTAR USUARIO---
CREATE OR ALTER PROCEDURE SEGURIDAD.sp_RegistrarUsuario
(
     -- Usuario
    @Correo VARCHAR(100),
    @ClaveHash VARCHAR(255),
    @IdRol INT,
	@IdMunicipio INT,

    -- Salidas
    @Resultado int OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;

    DECLARE @DescripcionRol VARCHAR(100);

    BEGIN TRY
        BEGIN TRANSACTION;
        -- Validar correo
        IF EXISTS (SELECT 1 FROM SEGURIDAD.Usuario WHERE Correo = @Correo)
        BEGIN
            SET @Mensaje = 'Ya existe un usuario con ese correo';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar en Usuario
        BEGIN
            INSERT INTO SEGURIDAD.Usuario (Correo,Clave_hash,idTipoUsuario,idMunicipio)
            VALUES (@Correo, @ClaveHash, @IdRol,@IdMunicipio); 
            SET @Resultado =SCOPE_IDENTITY();
        END
		

        COMMIT TRANSACTION; 
    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
    
END
GO



----PROCEDIMIENTO ALMACENADO PARA CREARRECURSO EN EL MARKETPLACE-----
CREATE OR ALTER PROCEDURE RECURSOS.sp_RegistrarRecursoMarketplace
(
    @TituloRecurso VARCHAR(100),
    @DescripcionRecurso text,
	@idSectorEconomico int,
    @idTipoRecurso int,
    @idEstadoRecurso int,
    @Precio decimal(18,2)=null,---si aplica o no

    @idUsuario int,
    -- Salidas
    @Resultado int OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT,
    @IdRecurso INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @IdRecurso = NULL;
   

    DECLARE  @DescripcionEstadoRecurso VARCHAR(100);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar estado
        SELECT @DescripcionEstadoRecurso=DescripcionEstadoRecurso
        FROM CATALOGOS.EstadoRecurso
        WHERE IdEstadoRecurso = @idEstadoRecurso;

        -- Insertar en la tabla
        IF @DescripcionEstadoRecurso = 'De Paga'
        BEGIN
            INSERT INTO RECURSOS.RecursosMarketplace(TituloRecurso,DescripcionRecurso,Id_tipoSectorEconomico,Id_tipoRecurso,Id_estadoRecurso,Precio)
            VALUES (@TituloRecurso,@DescripcionEstadoRecurso,@idSectorEconomico,@idTipoRecurso,@idEstadoRecurso,@Precio);
            SET @IdRecurso = SCOPE_IDENTITY(); --captura el ID
        END
        ELSE
        BEGIN
            INSERT INTO RECURSOS.RecursosMarketplace(TituloRecurso,DescripcionRecurso,Id_tipoSectorEconomico,Id_tipoRecurso,Id_estadoRecurso)
            VALUES (@TituloRecurso,@DescripcionEstadoRecurso,@idSectorEconomico,@idTipoRecurso,@idEstadoRecurso);
            SET @IdRecurso = SCOPE_IDENTITY(); --captura el ID
            SET @Resultado= @IdRecurso
        END

        -- Insertar TablaPivote
        BEGIN
            INSERT INTO RECURSOS.UsuarioRecursosMarketplace(Id_usuario,Id_recurso)
            VALUES(@idUsuario,@IdRecurso)
        END
		

        COMMIT TRANSACTION; 
    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
    
END
GO


----CONSULTA A BASE DE DATOS PARA MOSTRAR LOS RECURSOS QUE TIENE UN USUARIO EN SU MARKETPLACE---
Select 
IdRecurso,
TituloRecurso[Titulo],
DescripcionRecurso[Descripcion],
Precio,RutaArchivoRecurso,
NombreArchivoRecurso,
NombreTipoRecurso[TipoRecurso], 
NombreSector[SectorEconomico],
DescripcionEstadoRecurso[Estado],
FechaPublicacion
    from RECURSOS.UsuarioRecursosMarketplace Urm

    inner join RECURSOS.RecursosMarketplace Rm on Urm.Id_recurso=rm.IdRecurso
    inner join CATALOGOS.EstadoRecurso ER on Rm.Id_estadoRecurso=ER.IdEstadoRecurso
    inner join CATALOGOS.TipoRecurso Tr on Rm.Id_tipoRecurso=tr.IdTipoRecurso
    inner join CATALOGOS.TipoSectorEconomico tse on Rm.Id_tipoSectorEconomico=tse.IdTipoSectorEconomico
    where Urm.Id_usuario=1
    order by FechaPublicacion desc




----QUERY PARA MOSTRAR LA INFORMACION DEL MARKETPLACE A LAS MYPIMES

SELECT DISTINCT
    rm.IdRecurso,
    rm.TituloRecurso,
    urm.FechaPublicacion[FechaPublicacion],
    rm.Precio,
    rm.RutaArchivoRecurso,
    rm.NombreArchivoRecurso,
    CAST(rm.DescripcionRecurso AS NVARCHAR(MAX)) AS DescripcionRecurso,
    tr.NombreTipoRecurso,
    ts.NombreSector,
    er.DescripcionEstadoRecurso,
    COALESCE(
        CONCAT(per.NombrePersona, ' ', pn.ApellidoPersonaNatural),
        CONCAT(per.NombrePersona, ' ', pj.RazonSocial)
    ) AS NombrePublicador
FROM RECURSOS.UsuarioRecursosMarketplace urm
INNER JOIN RECURSOS.RecursosMarketplace rm 
    ON urm.Id_recurso = rm.IdRecurso
INNER JOIN CATALOGOS.TipoRecurso tr 
    ON rm.Id_tipoRecurso = tr.IdTipoRecurso
INNER JOIN CATALOGOS.TipoSectorEconomico ts 
    ON rm.Id_tipoSectorEconomico = ts.IdTipoSectorEconomico
INNER JOIN CATALOGOS.EstadoRecurso er 
    ON rm.Id_estadoRecurso = er.IdEstadoRecurso
INNER JOIN SEGURIDAD.Usuario u 
    ON urm.Id_usuario = u.IdUsuario
INNER JOIN CATALOGOS.Persona per 
    ON u.IdPersona = per.IdPersona
LEFT JOIN CATALOGOS.PersonaNatural pn 
    ON per.IdPersona = pn.Id_persona
LEFT JOIN CATALOGOS.PersonaJuridica pj 
    ON per.IdPersona = pj.Id_persona;
GO



