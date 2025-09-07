----PROCEDIMIENTOS ALMACENADOS

---PROCEDMIENTO ALMACENADO PARA INSERTAR USUARIO---
CREATE OR ALTER PROCEDURE SEGURIDAD.sp_RegistrarUsuario
(
    @NombrePersona VARCHAR(100),
    @IdMunicipio INT,

    -- Persona Jurídica
    @NumeroRuc VARCHAR(100) = NULL,
    @RazonSocial VARCHAR(100) = NULL,
	@idSectorEconomico int=NULL,
    -- Persona Natural
    @CedulaPersonaNatural VARCHAR(100) = NULL,
    @ApellidoPersonaNatural VARCHAR(100) = NULL,
    @IdGenero INT = NULL,

    -- Usuario
    @Correo VARCHAR(100),
    @ClaveHash VARCHAR(255),
    @IdRol INT,

    -- Salidas
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT,
    @IdUsuario INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @Resultado = 0;
    SET @IdUsuario = NULL;

    DECLARE @IdPersona INT, @DescripcionRol VARCHAR(100);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar rol
        SELECT @DescripcionRol = DescripcionRol
        FROM SEGURIDAD.RolUsuario
        WHERE IdRolUsuario = @IdRol;

        IF @DescripcionRol IS NULL

        BEGIN
            SET @Mensaje = 'Rol no válido';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar correo
        IF EXISTS (SELECT 1 FROM SEGURIDAD.Usuario WHERE Correo = @Correo)
        BEGIN
            SET @Mensaje = 'Ya existe un usuario con ese correo';
            ROLLBACK TRANSACTION;
            RETURN;
        END


        -- Insertar en Persona
        BEGIN
            INSERT INTO CATALOGOS.Persona (NombrePersona, Id_municipio)
            VALUES (@NombrePersona, @IdMunicipio);
            SET @IdPersona = SCOPE_IDENTITY(); --captura el ID
        END

        -- Insertar en PersonaJuridica o PersonaNatural según el rol
        IF @DescripcionRol = 'MIPYME'
        BEGIN
            INSERT INTO CATALOGOS.PersonaJuridica (NumeroRuc, Id_persona, RazonSocial,idTipoSectorEconomico)
            VALUES (@NumeroRuc, @IdPersona, @RazonSocial,@idSectorEconomico);
        END
        ELSE IF @DescripcionRol IN ('Experto', 'Diseñador')
        BEGIN
            INSERT INTO CATALOGOS.PersonaNatural (CedulaPerosonaNatural, Id_persona, ApellidoPersonaNatural, Id_genero)
            VALUES (@CedulaPersonaNatural, @IdPersona, @ApellidoPersonaNatural, @IdGenero);
        END
        ELSE
        BEGIN
            SET @Mensaje = 'Rol no válido. Solo se permite MiPyme, Disenador o Experto';
            ROLLBACK TRANSACTION;
            RETURN;
        END

		
        -- Insertar en Usuario
        BEGIN
            INSERT INTO SEGURIDAD.Usuario (idpersona, Correo, Clave_hash, idtipoUsuario)
            VALUES (@IdPersona, @Correo, @ClaveHash, @IdRol); 
            SET @IdUsuario = SCOPE_IDENTITY();
            SET @Resultado = @IdUsuario;
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

