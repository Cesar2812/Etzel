using LayerDomainModel;
using LayerUseCase.Marketplace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Controllers;


[Authorize]
public class MarketplaceController : Controller
{
    private readonly UCListarTipoRecurso _listarTipoRecusro;
    private readonly UCListarEstadoRecurso _listarEstadoRecurso;
    private readonly UCListarSectorEconomico _listarSectorEconomico;
   

    private readonly UCAgregarRecurso _ucagregarRecurso;
    private readonly UCSubirArchivoServidor _ucsubirArchivoServer;
    private readonly UCGuardarArchivoEnBD _uCGuardarArchivoEnBD;
    private readonly UCListarRecursosMarketplaceUsuario _ucListarRecursosUsuario;
    private readonly UCConvertirRecurso _ucConvertirRecurso;
    public MarketplaceController(UCListarTipoRecurso listarTipoRecurso,UCListarEstadoRecurso listarEstadoRecurso, UCListarSectorEconomico listarSectorEconomico,
        UCAgregarRecurso agregarRecurso, UCSubirArchivoServidor ucSubirArchivo, UCGuardarArchivoEnBD uCGuardarArchivoEnBD, UCListarRecursosMarketplaceUsuario ucListarRecursosUsuario,
        UCConvertirRecurso ucConvertirRecurso)
    {
        _listarTipoRecusro = listarTipoRecurso;
        _listarEstadoRecurso = listarEstadoRecurso; 
        _listarSectorEconomico = listarSectorEconomico;
        _ucagregarRecurso = agregarRecurso; 
        _ucsubirArchivoServer = ucSubirArchivo;
        _uCGuardarArchivoEnBD=uCGuardarArchivoEnBD;
        _ucListarRecursosUsuario=ucListarRecursosUsuario;
        _ucConvertirRecurso = ucConvertirRecurso;
    }


    //vista para visualizar el marketplace
    [Authorize(Roles = "MIPYME")]
    public IActionResult InicioMarketplace()//principal al entrar al marketplace
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


    #endregion


    [HttpPost]
    public async Task<IActionResult> AgregarRecursoPorUsuarioMarketplace(DMRecursosMarketplace objRecursoMarketplace)
    {
        var identity = (ClaimsIdentity)User.Identity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
        int idUsuario = int.Parse(claim.Value);

        int resultado = await _ucagregarRecurso.CrearRecursosMarketplace(objRecursoMarketplace, idUsuario);

        if (resultado > 0)
        {
            objRecursoMarketplace.IdRecurso = resultado;//captura el id del recurso insertado en la tabla

            string rutaArchivo = await _ucsubirArchivoServer.SubirRecurso(objRecursoMarketplace);
            objRecursoMarketplace.NombreArchivoRecurso = objRecursoMarketplace.archivo.FileName;
            objRecursoMarketplace.RutaArchivoRecurso= rutaArchivo;

            //guardar foto en base de datos
            bool guardado = await _uCGuardarArchivoEnBD.GuardarArchivoMarketplace(objRecursoMarketplace);
            if (guardado)
            {
                TempData["SuccessMessage"] = "Recurso Publicado Exitosamente";
                return View();
            }
            TempData["AbortMessage"] = "No se pudo publicar el recurso.";
            return View();
        }
        else
        {
            TempData["AbortMessage"] = "No se pudo publicar el recurso.";
            return View();
        }

    }


    [HttpPost]
    public async Task<IActionResult> MostrarRecursosMarketplaceUsuario()
    {
        var identity = (ClaimsIdentity)User.Identity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
        int idUsuario = int.Parse(claim.Value);

        var lista= await _ucListarRecursosUsuario.ListarRecursoMarketplaceUsuario(idUsuario);

        bool conversion;

        var listaRecurso=lista.Select(r=>new DMUsuarioRecursosMarketplace()
        {
            objRecursoMarketplace=new DMRecursosMarketplace()
            {
                IdRecurso=r.objRecursoMarketplace.IdRecurso,
                TituloRecurso=r.objRecursoMarketplace.TituloRecurso,
                DescripcionRecurso=r.objRecursoMarketplace.DescripcionRecurso,
                Precio=r.objRecursoMarketplace.Precio,
                RutaArchivoRecurso=r.objRecursoMarketplace.RutaArchivoRecurso,
                Base64=_ucConvertirRecurso.convertirBase64(Path.Combine(r.objRecursoMarketplace.RutaArchivoRecurso,r.objRecursoMarketplace.NombreArchivoRecurso),out conversion),
                Extension=Path.GetExtension(r.objRecursoMarketplace.NombreArchivoRecurso),
                objTipoRecurso=r.objRecursoMarketplace.objTipoRecurso,
                objTipoSectorEconomico=r.objRecursoMarketplace.objTipoSectorEconomico,
                objEstadoRecurso=r.objRecursoMarketplace.objEstadoRecurso,
            },
            FechaPublicacion=r.FechaPublicacion

        }).ToList();
        return Json(new { data = lista });
    }
}
