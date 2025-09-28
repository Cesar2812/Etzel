using LayerDomainModel;
using LayerUseCase.Marketplace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Application.Controllers;


[Authorize]
public class MarketplaceController : Controller
{
    private readonly UCListarTipoRecurso _listarTipoRecusro;
    private readonly UCListarEstadoRecurso _listarEstadoRecurso;
    private readonly UCListarSectorEconomico _listarSectorEconomico;
    private readonly UCListarTipoRecursoSector _listarTipoRecusroSector;
   

    private readonly UCAgregarRecurso _ucagregarRecurso;
    private readonly UCSubirArchivoServidor _ucsubirArchivoServer;
    private readonly UCGuardarArchivoEnBD _uCGuardarArchivoEnBD;
    private readonly UCListarRecursosMarketplaceUsuario _ucListarRecursosUsuario;
    private readonly UCConvertirRecurso _ucConvertirRecurso;
    private readonly UCMostrarRecursosMarketplace _ucMostrarRecurso;
    public MarketplaceController(UCListarTipoRecurso listarTipoRecurso,UCListarEstadoRecurso listarEstadoRecurso, UCListarSectorEconomico listarSectorEconomico,
        UCAgregarRecurso agregarRecurso, UCSubirArchivoServidor ucSubirArchivo, UCGuardarArchivoEnBD uCGuardarArchivoEnBD, UCListarRecursosMarketplaceUsuario ucListarRecursosUsuario,
        UCConvertirRecurso ucConvertirRecurso, UCMostrarRecursosMarketplace ucMostrarRecurso, UCListarTipoRecursoSector listarRecursoSector)
    {
        _listarTipoRecusro = listarTipoRecurso;
        _listarEstadoRecurso = listarEstadoRecurso; 
        _listarSectorEconomico = listarSectorEconomico;
        _ucagregarRecurso = agregarRecurso; 
        _ucsubirArchivoServer = ucSubirArchivo;
        _uCGuardarArchivoEnBD=uCGuardarArchivoEnBD;
        _ucListarRecursosUsuario=ucListarRecursosUsuario;
        _ucConvertirRecurso = ucConvertirRecurso;
        _ucMostrarRecurso = ucMostrarRecurso;
        _listarTipoRecusroSector=listarRecursoSector;
    }

    public IActionResult Bienvenida()
    {
        string Rol = User.FindFirstValue(ClaimTypes.Role);
        ViewBag.RolUsuario = Rol;
        return View();
    }

    ////vista para visualizar el marketplace
    [Authorize(Roles = "MIPYME")]
    public IActionResult InicioMarketplaceMiPymes()
    {
        return View();
    }


    [Authorize(Roles = "Artista,Profesional Independiente,Artesano")]
    public IActionResult InicioMarketplacePerfilPorUsuario()
    {
        return View();
    }


    #region RecursosDeMuestraEnFomulario
    [HttpGet]
    public async Task<IActionResult> ListarTipoRecurso()
    {
        List<DMTipoRecurso> lista = await _listarTipoRecusro.ListarTipoRecuro();
        return Json(new { listaTipoRecurso = lista });
    }

    [HttpGet]
    public async Task<IActionResult> ListarEstadoRecurso()
    {
        List<DMEstadoRecurso> lista = await _listarEstadoRecurso.ListarEstadoRecurso();
        return Json(new { listaEstadoRecurso = lista });
    }

    [HttpGet]
    public async Task<IActionResult> ListarSectorEconomico()
    {
        List<DMTipoSectorEconomico> lista = await _listarSectorEconomico.ListarSectorEconomico();
        return Json(new { listaSectorEconomico = lista });
    }

     [HttpGet]
     public async Task<IActionResult> ObtenerTipoRecursoPorSector(int idSector)
     {
        List<DMTipoRecurso> lista = await _listarTipoRecusroSector.ListarRecursoSector(idSector);
        return Json(new { listaRecursoSector = lista });
     }

    #endregion


    #region Metodos
    [HttpPost]
    public async Task<IActionResult> AgregarRecursoPorUsuarioMarketplace(string objRecursoMarketplace,IFormFile archivo)
    {
        // Deserializar el JSON recibido
        var recurso = JsonConvert.DeserializeObject<DMRecursosMarketplace>(objRecursoMarketplace);

        var identity = (ClaimsIdentity)User.Identity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
        int idUsuario = int.Parse(claim.Value);

        int resultado = await _ucagregarRecurso.CrearRecursosMarketplace(recurso, idUsuario);

        if (resultado > 0)
        {
            recurso.IdRecurso = resultado;//captura el id del recurso insertado en la tabla
            recurso.archivo = archivo;

            string rutaArchivo = await _ucsubirArchivoServer.SubirRecurso(recurso);
            recurso.NombreArchivoRecurso = recurso.archivo.FileName;
            recurso.RutaArchivoRecurso= rutaArchivo;

            //guardar foto en base de datos
            bool guardado = await _uCGuardarArchivoEnBD.GuardarArchivoMarketplace(recurso);
            if (guardado)
            {
                return Json(new { success = true, message = "Recurso publicado exitosamente", idGenerado=recurso.IdRecurso});
            }
            return Json(new { success = false, message = "No se pudo publicar el recurso" });
        }
        else
        {
            return Json(new { success = false, message = "No se pudo publicar el recurso" });
        }

    }



    //por usuario especifico 
    [HttpGet]
    public async Task<IActionResult> MostrarRecursosMarketplaceUsuario(int idSectorEconomico, int idTipoRecurso)
    {
        var identity = (ClaimsIdentity)User.Identity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
        int idUsuario = int.Parse(claim.Value);

        var lista = await _ucListarRecursosUsuario.ListarRecursoMarketplaceUsuario(idUsuario);

        var listaRecurso = lista.Select(r => new DMUsuarioRecursosMarketplace()
        {
            objRecursoMarketplace = new DMRecursosMarketplace()
            {
                IdRecurso = r.objRecursoMarketplace.IdRecurso,
                TituloRecurso = r.objRecursoMarketplace.TituloRecurso,
                DescripcionRecurso = r.objRecursoMarketplace.DescripcionRecurso,
                Precio = r.objRecursoMarketplace.Precio,
                RutaArchivoRecurso = r.objRecursoMarketplace.RutaArchivoRecurso, // ya es relativa
                NombreArchivoRecurso = r.objRecursoMarketplace.NombreArchivoRecurso,
                objTipoRecurso = r.objRecursoMarketplace.objTipoRecurso,
                objTipoSectorEconomico = r.objRecursoMarketplace.objTipoSectorEconomico,
                objEstadoRecurso = r.objRecursoMarketplace.objEstadoRecurso,
            },
            FechaPublicacion = r.FechaPublicacion
        }).ToList();

        return Json(new { data = listaRecurso });
    }



    //de forma General a las MiPymes
    [HttpPost]
    public async Task<IActionResult> MostrarRecursosMarketplace(int idSectorEconomico, int idTipoRecurso)
    {
        var lista = await _ucMostrarRecurso.ListarRecursoMarketplace();

        var listaRecurso = lista.Select(r => new DMUsuarioRecursosMarketplace()
        {
            objRecursoMarketplace = new DMRecursosMarketplace()
            {
                IdRecurso = r.objRecursoMarketplace.IdRecurso,
                TituloRecurso = r.objRecursoMarketplace.TituloRecurso,
                DescripcionRecurso = r.objRecursoMarketplace.DescripcionRecurso,
                Precio = r.objRecursoMarketplace.Precio,
                RutaArchivoRecurso = r.objRecursoMarketplace.RutaArchivoRecurso,
                NombreArchivoRecurso = r.objRecursoMarketplace.NombreArchivoRecurso,
                objTipoRecurso = r.objRecursoMarketplace.objTipoRecurso,
                objTipoSectorEconomico = r.objRecursoMarketplace.objTipoSectorEconomico,
                objEstadoRecurso = r.objRecursoMarketplace.objEstadoRecurso,
            },
            objUsuario = r.objUsuario,
            FechaPublicacion = r.FechaPublicacion
        })
        // Filtro condicional
        .Where(r =>
            (idSectorEconomico == 0 || r.objRecursoMarketplace.objTipoSectorEconomico.IdTipoSectorEconomico == idSectorEconomico) &&
            (idTipoRecurso == 0 || r.objRecursoMarketplace.objTipoRecurso.IdTipoRecurso == idTipoRecurso)
        ).ToList();

        return Json(new { data = listaRecurso });
    }

    #endregion
}
