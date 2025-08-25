# Etzel

   ### Requisitos Previos
        -Configuración del Entorno de Desarrollo
        Para trabajar en este proyecto se requiere tener instalado y configurado lo siguiente:

        Visual Studio 2022 (versión Community o superior) con la carga de trabajo:
            ASP.NET y desarrollo web
            .NET SDK (versión compatible con el proyecto, verificar en global.json o .csproj) en este caso de este proyecto es la 9.0
            Editor de Codigo Cursor basado en Visual Studio Code ----PENDIENTE DE PONER LA VERSION----

        Gestor de base de datos relacional: SQL Server 2019.
           #Herramientas gráficas para administracion de la misma:
            SQL Server Management Studio (SSMS) 2019.
            Manejador de Base De Datos de Visual Studio 2022
            Azure Data Studio (última versión).

        Git instalado y configurado para el control de versiones y trabajo colaborativo entre los dos desarrolladores.
            Cuenta en GitHub para acceder al repositorio.

        IIS Express (incluido con Visual Studio 2022) para pruebas locales desde el IDE.
        Uso del comando donet -run para las pruebas locales en el editor de Cursor
        Conexión a internet para instalación de paquetes NuGet.

   ### Instrucciones de Instalación
        1) Clonar el repositorio del proyecto
            git clone https://github.com/Cesar2812/Etzel
        2)Abrir la solución en Visual Studio 2022 o Editor de codigo similar:
            Archivo -> Abrir -> Proyecto/Solución -> seleccionar .sln.
        3)Restaurar paquetes NuGet:
            En Visual Studio: Menú Herramientas -> Administrador de paquetes NuGet -> Restaurar paquetes.
            O Comando dotnet restore si es abierto en Visual Studio Code.
        4)Instalar SQL Server 2019 y crear la base de datos según los scripts proporcionados.
            Instalar SSMS 2019 o Azure Data Studio para administración de la base de datos.
  
   ### Pasos de Configuracion 
     1)Configurar la base de datos:
        Crear un usuario SQL con permisos mínimos (app_user) para la conexión desde la aplicación.
        Ejecutar los scripts de creación de tablas incluidos en /DataBase/Scripts.
     2) Configurar la cadena de conexión:
        En appsettings.json, actualizar la cadena de conexión "DefaultConnection" con los detalles de su servidor SQL y base de datos usando variables de entorno.
        Ejemplo:
        "ConnectionStrings": {
            "DefaultConnection":
            "Server=YOUR_SERVER;Database=DataBaseName;User Id=userName;Password=YOUR_PASSWORD
            ;TrustServerCertificate=True;"
            }
    3) Configurar variables de entorno:
        Establecer las siguientes variables de entorno en su sistema o en el entorno de desarrollo:
            - ConnectionStrings__Default

    4) Configurar ramas en Git:
        master: rama principal (producción, MVP).
        desarrollo: rama de trabajo para nuevas funciones.
    
    5) Configurar IIS Express (por defecto en Visual Studio):
        En el menú de ejecución, seleccionar IIS Express y el proyecto principal.


    
   ### Solución de Problemas Comunes
        1)Error de conexión a la base de datos:
            Verificar que SQL Server esté en ejecución.
            Revisar usuario, contraseña y nombre del servidor en appsettings.json.
        2)Error al restaurar NuGet:
        Ejecutar dotnet restore desde la terminal de Visual Studio.
        Verificar conexión a internet.
   