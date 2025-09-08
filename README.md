####                                                                       ETZEL

# Proposito de la plataforma Etzel
    #El proposito de la creacion de esta plataforma es implementar un medio web donde Mypymes en sectores como textiles,muebles,cueros y sus derivados,
    tambien en sectores como alimentos y bebidas, puedan ser ayudadas a estandarizar y optimizar sus procesos productivos con el fin de mejorar la eficiencia,
    la gestion de costos y  la calidad de sus productos finales mediante una calculadora de cantidades y porciones de materiales o insumos necesarios
    para la fabricacion de uno de sus productos mediante una formulacion inicial.Esto con el fin de ofrecer a las Mypymes una herramienta centralizada y colaborativa, cuyo componente 
    crucial sera la integracion de un Marketplace convirtiendo a este sistema o plataforma en un repositororio de conocimiento compartido
    en el cual las Mypymes, Disenadores y Expertos puedan publicar su recursos como patrones, moldes, formularios, o plantillas(estableciendolos como medios descargables o de paga).
    Integrando tambien un asistente virtual sobre esta plataforma para que funcione como un medio de sugerencias para mejorar la estadarizacion de los procesos realizados
    por estas MyPymes.

    El impacto social de esta plataforma digital en Nicaragua sería significativo y multidimensional, afectando a la economía, el empleo y el desarrollo de las comunidades.


#  Requisitos Previos para ejecucion e instalacion del proyecto
            -Configuración del Entorno de Desarrollo
            Para trabajar en este proyecto se requiere tener instalado y configurado las siguientes herramientas:

             Visual Studio 2022 (versión Community o superior) con la carga de trabajo:
                ASP.NET y desarrollo web
                .NET SDK (versión compatible con el proyecto, verificar en global.json o .csproj) en este caso de este proyecto es la 9.0

            Gestor de base de datos relacional: SQL Server 2019.
               #Herramientas gráficas para administracion de la misma:
                SQL Server Management Studio (SSMS) 2021 o 2019 segun su preferencia.
                Manejador de Base De Datos de Visual Studio 2022
                Azure Data Studio (última versión).

            IIS Express (Servidor web local incluido con Visual Studio 2022) para pruebas locales desde el IDE.
            Conexión a internet para instalación de paquetes NuGet y restauracion de paquetes nugets.

#       Instrucciones de Instalación y Configuracion
                Estas herramientas son unicamente para usarse en sistemas opertaivos Windows, pero cabe recalcar que la app
                es multiplaforma y puede ser deplagada en servidores Linux o Windows(Nota: si se quisiera llevar a futuras implemnetaciones y produccion)
        
        Antes de instalar estas herramientas en Windows cabe senalar que hay que tener instalado Microsft Visual C++ 2015-222 Redistribuible X64 
        o dependiendo la arquitectura de su sistema operativo Windows ya que estas herramientas que se instalaran al ser de Microsoft necesitan
        este intermediario dentro del sistema para poder ser ejecutadas.

        1)Instalar VisualStudio en su version 2022 este puede ser descargado desde la pagina oficial de VisualStudio https://visualstudio.microsoft.com/es/vs/  
        eligiendo la version Community o Professional, luego de esto elegir el marco de trabajo "Desarrollo de ASP.NET  y Web"
        

        2)Instalar SQL Server 2019 y crear la base de datos según los scripts proporcionados.
            Instalar SSMS(Management Studio) 2019 o 2021 o Azure Data Studio para administración de la base de datos, estas herramientas pueden ser descargadas desde las siguientes paginas
            https://www.visual-expert.com/ES/visual-expert-blog/posts-2020/guia-instalacion-sql-server-2019-visual-expert.html
            https://learn.microsoft.com/es-es/ssms/release-notes-20

           
        3)Configurar la base de datos:
            Crear un usuario SQL con permisos mínimos (app_user) para la conexión(esto con el fin de proteger la base 
            de datos contra accesos no autorizados,evitando asi el uso de sa,root o admin) siguiendo los pasos en los scripts de creación de tablas incluidos en /DataBase/Scripts.
            Ejecutar los scripts de los procedimientos almacenados y todos los Transact-SQL


        4) En Visual Studio   2022 Clonar el repositorio del proyecto  en la opcion de Git->Clonar Repositorio y ponemos las URl del Repositorio
            https://github.com/Cesar2812/Etzel

        5)Estando en la solución en Visual Studio 2022 :
            Restaurar paquetes NuGet:
            Menú Herramientas -> Administrador de paquetes NuGet -> Restaurar paquetes.
      
        6) Configurar la cadena de conexión:
            En appsettings.json, actualizar la cadena de conexión "ConnectionStrings" con los detalles de su servidor SQL y base de datos usando variables de entorno.
            Ejemplo:Crear el string de conexion como variable de entorno dentro del sistema operativo 
            $env:ConnectionStrings__ConnectionDataBase = "Server=nombredelservidor o ip;Database=MiBase;User Id=user;Password=password;TrustServerCertificate=True;Encrypt=False;"
            sustituyendo user y password por los creados desde el script de SQL.

                Quizas se pregunte porque esta manera de hacerlo, el objetivo de esto es tratar de aplicar Seguridad en la aplicacion evitando guardar las 
                credenciales de acceso en el codigo fuente o en el archivo appsettings, usando archivos protegidos con variables de entorno locales dentro del
                entorno de desarrollo, evitando tambien no exponer el puerto de SQL Server a redes externas; el acceso está limitado a la máquina local del equipo.
                Las conexiones están limitadas a entornos locales para prevenir accesos externos no autorizados.

            7)Configurar el servidor o carpeta de imagenes,creando una carpeta en el directorio de
             creacion del proyecto y luego crear una varible de entorno con esta ruta, para que el archivo appsettings pueda reconocerla, crearla de la 
             siguiente manera $env:Configuracion__RutaServidor= ruta de la carpeta dentro del directorio


        8) Configurar IIS Express (por defecto en Visual Studio 2022)para ejecucion del proyecto en entorno local de pruebas:
            En el menú de ejecución, seleccionar IIS Express y el proyecto principal.
            Servidor de pruebas: IIS Express desde Visual Studio.


   ### Solución de Problemas Comunes
        1)Error de conexión a la base de datos:
            Verificar que SQL Server esté en ejecución.
            Revisar usuario, contraseña y nombre del servidor en el visualizador de variables de entorno.
        
   