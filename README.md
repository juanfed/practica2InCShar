# Practica 2 - Programacion a 3 Capas ADO.NET (Web MVC y CRUD)

## Descripcion

Aplicacion web ASP.NET Web Forms (.NET Framework 4.7.2) para la gestion de estudiantes del ITM. Implementa operaciones CRUD (Crear, Leer, Actualizar) utilizando arquitectura de 3 capas con ADO.NET y SQL Server.

## Estructura del Proyecto

```
Practica 2/
├── BaseDeDatos/
│   └── bdEstudITM.sql          # Script SQL para crear la BD, tablas y stored procedures
├── guia/
│   └── guia 2.pdf              # Documento guia de la practica
├── libCnxBD/                   # Capa de Datos - Biblioteca de clases
│   └── libCnxBD/
│       └── clsConexion.cs      # Clase clsCnxBD: conexion, consultas y comandos SQL
├── webSIA/                     # Capa de Presentacion y Logica de Negocio
│   └── webSIA/
│       ├── Clases/
│       │   └── clsEstudiante.cs    # Clase clsEstudiante: logica de negocio del estudiante
│       ├── Imagenes/
│       │   └── lupa.png            # Icono de busqueda
│       ├── frmEstudiante.aspx      # Formulario web (Vista)
│       ├── frmEstudiante.aspx.cs   # Code-behind del formulario (Controlador)
│       └── Web.config              # Configuracion de la aplicacion web
└── README.md
```

## Arquitectura de 3 Capas

1. **Capa de Datos (`libCnxBD`)**: Biblioteca de clases que encapsula la conexion y operaciones con SQL Server mediante ADO.NET (SqlConnection, SqlCommand, SqlDataReader, SqlParameter).

2. **Capa de Logica de Negocio (`clsEstudiante`)**: Clase que contiene las reglas de negocio, validaciones y los metodos para interactuar con los stored procedures (buscar, grabar, modificar).

3. **Capa de Presentacion (`frmEstudiante`)**: Formulario web ASP.NET que permite al usuario interactuar con el sistema mediante un menu con opciones de Buscar, Agregar, Modificar, Grabar y Cancelar.

## Funcionalidades

- **Buscar**: Busca un estudiante por su codigo/carne
- **Agregar**: Registra un nuevo estudiante en la base de datos
- **Modificar**: Actualiza los datos de un estudiante existente
- **Grabar**: Ejecuta la operacion seleccionada (agregar o modificar)
- **Cancelar**: Limpia el formulario y cancela la operacion en curso

## Stored Procedures

- `USP_Est_BuscarXcodigo`: Busca un estudiante por codigo
- `USP_Est_Grabar`: Inserta un nuevo estudiante (genera codigo automaticamente)
- `USP_Est_Modificar`: Actualiza los datos de un estudiante existente

## Configuracion de la Base de Datos

### 1. Crear la base de datos

Ejecutar el script `BaseDeDatos/bdEstudITM.sql` en SQL Server Management Studio (SSMS). Este script crea:
- La base de datos `bdEstudITM`
- La tabla `tblEstudiante` con datos de ejemplo
- Los 3 stored procedures necesarios

### 2. Configurar la cadena de conexion

La cadena de conexion se encuentra en el archivo `webSIA/webSIA/Clases/clsEstudiante.cs`, en ambos constructores:

```csharp
strCnx = "Data Source = .\\SQLEXPRESS; Initial Catalog = bdEstudITM; Integrated Security = SSPI;";
```

**Que modificar segun tu entorno:**

| Parametro | Valor actual | Que cambiar |
|-----------|-------------|-------------|
| `Data Source` | `.\\SQLEXPRESS` | Nombre de tu servidor SQL. Ejemplos: `.` (local default), `.\\SQLEXPRESS` (SQL Express), `MIPC\\SQLEXPRESS`, `localhost` |
| `Initial Catalog` | `bdEstudITM` | Nombre de la base de datos (no cambiar si usaste el script tal cual) |
| `Integrated Security` | `SSPI` | Usa autenticacion de Windows. Si necesitas usuario/password de SQL Server, reemplazar por: `user id = TU_USUARIO; password = TU_PASSWORD;` |

**Ejemplo con autenticacion de Windows (por defecto):**
```
Data Source = .\\SQLEXPRESS; Initial Catalog = bdEstudITM; Integrated Security = SSPI;
```

**Ejemplo con autenticacion de SQL Server:**
```
Data Source = .\\SQLEXPRESS; Initial Catalog = bdEstudITM; user id = sa; password = tu_password;
```

### 3. Referencia a libCnxBD

El proyecto `webSIA` debe tener una referencia a la DLL generada por el proyecto `libCnxBD`. Si la referencia no esta configurada:
1. En Visual Studio, clic derecho en `webSIA` > Agregar > Referencia
2. Examinar y seleccionar `libCnxBD/libCnxBD/bin/Debug/libCnxBD.dll`

## Tipos de Documentos

| ID | Tipo |
|----|------|
| 1 | Cedula Ciudadania |
| 3 | Cedula Extranjeria |
| 6 | Pasaporte |
| 7 | NIT |
| 8 | NUIP |
| 9 | Permiso de Proteccion Temporal |

## Programas Academicos

| ID | Programa |
|----|----------|
| 10 | Tecnologia en Informatica Musical |
| 11 | Ingenieria en Diseno Industrial |
| 70 | Quimica Industrial |
| 71 | Ingenieria Biomedica |
| 100 | Tecn. Desarrollo de Software |
| 101 | Ing. de Sistemas |

## Requisitos

- Microsoft Visual Studio 2022
- .NET Framework 4.7.2
- SQL Server 2005/2008 o superior (SQL Server Express funciona)
- SQL Server Management Studio (SSMS) para ejecutar el script SQL
