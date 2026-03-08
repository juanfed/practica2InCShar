Create DATABASE bdEstudITM
go
USE bdEstudITM
go

CREATE TABLE tblEstudiante
(
    Codigo int Primary Key NOT NULL,
    nroDoc int NOT NULL,
    idTDoc int NOT NULL,
    Nombre varchar(50) NOT NULL,
    Apellido varchar(50) NOT NULL,
    idPRO int NOT NULL,
    Observac varchar(500) NULL,
    Activo bit default 1 NOT NULL
)
go

-- ============== ESTudiante ==============
INSERT INTO tblEstudiante VALUES
(30001, 10, 10001000, 7, 'Juan P.', 'Aristizábal Cardona', 1, 'Ppto Particip'),
(30002, 11, 10001001, 8, 'Mario A.', 'Martínez Rios', 1, 'Fondo EPM'),
(30003, 70, 10001002, 1, 'Natalia', 'Castrillón Caguán', 1, null),
(30004, 71, 10001003, 1, 'Rubén Darío', 'Soto Rivera', 1, 'Pruebas de todo'),
(30005, 100, 10001004, 3, 'Scott', 'Rangún Suteu', 1, 'Reserva'),
(30006, 10, 10001005, 9, 'Jazmín A.', 'Angarita Marín', 1, 'Ppto Particip.'),
(30007, 101, 10001006, 6, 'José H.', 'Soto Diaz', 1, 'Suspende Matríc')
go

-- Stored Procedure: Buscar por código
CREATE PROCEDURE USP_Est_BuscarXcodigo
@intCodigo int
AS
BEGIN
    SELECT idPRO codProg, NroDoc, idTDoc TipoDoc, Nombre, Apellido, Activo, isnull(Observac,'') Observac
    FROM tblEstudiante
    WHERE Codigo = @intCodigo
END
go

-- Stored Procedure: Grabar
CREATE PROCEDURE USP_Est_Grabar
@intIdProg   int,
@intNroDoc   int,
@intTipoDoc  int,
@strNombre   VARCHAR(50),
@strApelli   VARCHAR(50),
@bitActivo   BIT,
@strObserv   VARCHAR(500)
As
BEGIN
    IF EXISTS( SELECT * FROM tblEstudiante
               WHERE NroDoc = @intNroDoc )
    BEGIN
        SELECT -1 AS Rpta
        Return
    END
    ELSE
    BEGIN
        BEGIN TRANSACTION tx
        Declare @intNewCod int
        Set @intNewCod = ( select max( Codigo ) + 1
                           from tblEstudiante )

        INSERT INTO tblEstudiante VALUES ( @intNewCod,
            @intIdProg, @intNroDoc, @intTipoDoc,
            @strNombre, @strApelli, @bitActivo, @strObserv );
        IF ( @@ERROR > 0 )
        BEGIN
            ROLLBACK TRANSACTION tx
            SELECT 0 AS Rpta
            Return
        END
        COMMIT TRANSACTION tx
        SELECT @intNewCod AS Rpta
        Return
    END
END
go

-- Stored Procedure: Modificar
CREATE PROCEDURE USP_Est_Modificar
@intCodigo   int,
@intIdProg   int,
@intNroDoc   int,
@intTipoDoc  int,
@strNombre   VARCHAR(50),
@strApelli   VARCHAR(50),
@bitActivo   BIT,
@strObserv   VARCHAR(500)
As
BEGIN
    IF EXISTS( SELECT * FROM tblEstudiante
               WHERE NroDoc = @intNroDoc and codigo <> @intCodigo )
    BEGIN
        SELECT -1 AS Rpta
        Return
    END
    ELSE
    BEGIN
        BEGIN TRANSACTION tx
        UPDATE tblEstudiante SET
            idPRO = @intIdProg,       NroDoc = @intNroDoc,
            idTDoc = @intTipoDoc,     Nombre = @strNombre,
            Apellido = @strApelli,    Activo = @bitActivo,
            Observac = @strObserv
        WHERE Codigo = @intCodigo
        IF ( @@ERROR > 0 )
        Begin
            ROLLBACK TRANSACTION tx
            SELECT 0 AS Rpta
            Return
        End
        COMMIT TRANSACTION tx
        SELECT @intCodigo AS Rpta
        Return
    END
END
go