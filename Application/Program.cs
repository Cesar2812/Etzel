using LayerAdapters;
using LayerDataAccess;
using LayerDataAccess.DAMarketplace;
using LayerDataAccess.DAUsuario;
using LayerUseCase.Interface;
using LayerUseCase.Marketplace;
using LayerUseCase.Usuario;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//agregando servicios de Autenticacion y esquema de autenticacion y agregando opciones de cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/LoginAcceso/Index";//el path de logueo osea pagina de acceso
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);//tiempo de expiracion de la Cookie osea se muere en 20 minutos

    });




// Cargar configuración desde JSON y luego sobrescribe con variables de entorno
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.Configure<Conection>(builder.Configuration.GetSection("ConnectionStrings"));//inyeccion de dependencia a la base de datos
builder.Services.Configure<Ruta>(builder.Configuration.GetSection("Configuracion"));//inyecccion de depencias al servidor de imagenes o carpeta para almacenar imagenes y archivos
builder.Services.Configure<RutaArchivos>(builder.Configuration.GetSection("ConfigServer"));

//Inyeccion de depencias inyectando las clases que van a implementar una interface del caso de uso de Usuarios
//CASO DE USO DEL USUARIO
builder.Services.AddScoped<ICrearCuenta, CrearCuenta>();
builder.Services.AddScoped<ICambiarClave, CambiarClave>();
builder.Services.AddScoped<IGuardarFoto, GuardarFoto>();
builder.Services.AddScoped<IRestablecerClave,RestablecerClave>();
builder.Services.AddScoped<IRecibirCorreo, ReceiveEmail>();
builder.Services.AddScoped<IListar, Listar>();
builder.Services.AddScoped<IGuardarFotoServidor, GuardarFotoEnServidor>();
//Lista de Roles
builder.Services.AddScoped<IListarRol, ListarRol>();
//Lista de departamentos y municipios
builder.Services.AddScoped<IListarDepartamento, ListarDepartamento>();
builder.Services.AddScoped<IObtenerMunicipio, ObtenerMunicipio>();
//lista de SectoresEconomicos 
builder.Services.AddScoped<IListarSectorEconomico, ObtenerSectorEconomico>();
//lista de Genero
builder.Services.AddScoped<IListarGenero, ObtenerGenero>();


builder.Services.AddScoped<UCcrearCuentaUser>();
builder.Services.AddScoped<UCGuardarFoto>();
builder.Services.AddScoped<UCRestablecerClave>();
builder.Services.AddScoped<UCCambiarClave>();
builder.Services.AddScoped<UCListarUsuario>();
builder.Services.AddScoped<UCSubirFotoServidor>();

builder.Services.AddScoped<UCListarRol>();
builder.Services.AddScoped<UCObtenerDepartamento>();
builder.Services.AddScoped<UCObtenerMunicipio>();

builder.Services.AddScoped<UCListarSectorEconomico>();

builder.Services.AddScoped<UCListarGenero>();


//CASOS DE USOS DEL MARKETPLACE
builder.Services.AddScoped<IListarEstadoRecurso,ObtenerEstadoRecurso>();
builder.Services.AddScoped<IListarTipoRecusro, ObtenerTipoRecurso>();

builder.Services.AddScoped<ICrearRecursoMarketplace, CrearRecursoMarketplace>();
builder.Services.AddScoped<IGuardarArchivoBD, GuardarArchivoBD>();
builder.Services.AddScoped<IGuardarRecursoServidor,GuardarArchivoDeRecurso>();


builder.Services.AddScoped<UCListarEstadoRecurso>();
builder.Services.AddScoped<UCListarTipoRecurso>();

builder.Services.AddScoped<UCAgregarRecurso>();
builder.Services.AddScoped<UCGuardarArchivoEnBD>();
builder.Services.AddScoped<UCSubirArchivoServidor>();


































var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();//usando autenticacion
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
