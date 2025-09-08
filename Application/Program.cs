using LayerDataAccess;
using LayerDataAccess.DALocalizacion;
using LayerDataAccess.DARol;
using LayerDataAccess.DAUsuario;
using LayerUseCase.Interface;
using LayerUseCase.Localizacion;
using LayerUseCase.Rol;
using LayerUseCase.Usuario;
using LayerAdapters;
using LayerDataAccess.DASectorEconomico;
using LayerUseCase.SectorEconomico;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Cargar configuración desde JSON y luego sobrescribe con variables de entorno
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.Configure<Conection>(builder.Configuration.GetSection("ConnectionStrings"));//inyeccion de dependencia a la base de datos
builder.Services.Configure<Ruta>(builder.Configuration.GetSection("Configuracion"));//inyecccion de depencias al servidor de imagenes o carpeta para almacenar imagenes

//Inyeccion de depencias inyectando las clases que van a implementar una interface del caso de uso de Usuarios
//casos de uso de usuarios
builder.Services.AddScoped<ICrearCuenta, CrearCuenta>();
builder.Services.AddScoped<ICambiarClave, CambiarClave>();
builder.Services.AddScoped<IGuardarFoto, GuardarFoto>();
builder.Services.AddScoped<IRestablecerClave,RestablecerClave>();
builder.Services.AddScoped<IRecibirCorreo, ReceiveEmail>();
builder.Services.AddScoped<IListar, Listar>();
builder.Services.AddScoped<IGuardarFotoServidor, GuardarFotoEnServidor>();

//lista de Roles
builder.Services.AddScoped<IListarRol, ListarRol>();

//Lista de departamentos y municipios
builder.Services.AddScoped<IListarDepartamento, ListarDepartamento>();
builder.Services.AddScoped<IObtenerMunicipio, ObtenerMunicipio>();

//lista de SectoresEconomicos 
builder.Services.AddScoped<IListarSectorEconomico, ObtenerSectorEconomico>();



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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
