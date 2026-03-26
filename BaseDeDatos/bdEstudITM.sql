CREATE DATABASE bdEstudITM
GO
USE bdEstudITM
GO

CREATE TABLE tblEstudiante
(
    Codigo   int          PRIMARY KEY NOT NULL,
    nroDoc   int          NOT NULL,
    idTDoc   int          NOT NULL,
    Nombre   varchar(50)  NOT NULL,
    Apellido varchar(50)  NOT NULL,
    idPRO    int          NOT NULL,
    Observac varchar(500) NULL,
    Activo   bit          DEFAULT 1 NOT NULL
)
GO

-- ============== INSERTAR ESTUDIANTES ==============
INSERT INTO tblEstudiante (Codigo, nroDoc, idTDoc, Nombre, Apellido, idPRO, Observac, Activo) VALUES
(30001, 10001000, 7,  'Juan P.',      'Aristizabal Cardona', 10,  'Ppto Particip',    1),
(30002, 10001001, 8,  'Mario A.',     'Martinez Rios',       11,  'Fondo EPM',        1),
(30003, 10001002, 1,  'Natalia',      'Castrillon Caguan',   70,  NULL,               1),
(30004, 10001003, 1,  'Ruben Dario',  'Soto Rivera',         71,  'Pruebas de todo',  1),
(30005, 10001004, 3,  'Scott',        'Rangun Suteu',        100, 'Reserva',          1),
(30006, 10001005, 9,  'Jazmin A.',    'Angarita Marin',      10,  'Ppto Particip.',   1),
(30007, 10001006, 6,  'Jose H.',      'Soto Diaz',           101, 'Suspende Matric',  1)
GO

-- ============== SP: Buscar por Codigo ==============
CREATE PROCEDURE USP_Est_BuscarXcodigo
    @intCodigo int
AS
BEGIN
    SELECT
        idPRO       AS codProg,
        NroDoc,
        idTDoc      AS TipoDoc,
        Nombre,
        Apellido,
        Activo,
        ISNULL(Observac, '') AS Observac
    FROM tblEstudiante
    WHERE Codigo = @intCodigo
END
GO

-- ============== SP: Grabar ==============
CREATE PROCEDURE USP_Est_Grabar
    @intIdProg  int,
    @intNroDoc  int,
    @intTipoDoc int,
    @strNombre  VARCHAR(50),
    @strApelli  VARCHAR(50),
    @bitActivo  BIT,
    @strObserv  VARCHAR(500)
AS
BEGIN
    -- Verificar si ya existe un estudiante con ese numero de documento
    IF EXISTS (SELECT 1 FROM tblEstudiante WHERE NroDoc = @intNroDoc)
    BEGIN
        SELECT -1 AS Rpta
        RETURN
    END

    BEGIN TRY
        BEGIN TRANSACTION

            DECLARE @intNewCod int
            SET @intNewCod = (SELECT ISNULL(MAX(Codigo), 30000) + 1 FROM tblEstudiante)

            INSERT INTO tblEstudiante (Codigo, nroDoc, idTDoc, Nombre, Apellido, idPRO, Observac, Activo)
            VALUES (@intNewCod, @intNroDoc, @intTipoDoc, @strNombre, @strApelli, @intIdProg, @strObserv, @bitActivo)

        COMMIT TRANSACTION
        SELECT @intNewCod AS Rpta

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SELECT 0 AS Rpta
    END CATCH
END
GO

-- ============== SP: Modificar ==============
CREATE PROCEDURE USP_Est_Modificar
    @intCodigo  int,
    @intIdProg  int,
    @intNroDoc  int,
    @intTipoDoc int,
    @strNombre  VARCHAR(50),
    @strApelli  VARCHAR(50),
    @bitActivo  BIT,
    @strObserv  VARCHAR(500)
AS
BEGIN
    -- Verificar si el nroDoc ya lo usa otro estudiante distinto
    IF EXISTS (SELECT 1 FROM tblEstudiante WHERE NroDoc = @intNroDoc AND Codigo <> @intCodigo)
    BEGIN
        SELECT -1 AS Rpta
        RETURN
    END

    BEGIN TRY
        BEGIN TRANSACTION

            UPDATE tblEstudiante SET
                idPRO    = @intIdProg,
                NroDoc   = @intNroDoc,
                idTDoc   = @intTipoDoc,
                Nombre   = @strNombre,
                Apellido = @strApelli,
                Activo   = @bitActivo,
                Observac = @strObserv
            WHERE Codigo = @intCodigo

        COMMIT TRANSACTION
        SELECT @intCodigo AS Rpta

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SELECT 0 AS Rpta
    END CATCH
END
GO
