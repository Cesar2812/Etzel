using LayerDomainModel;
using LayerUseCase.Marketplace;
using LayerUseCase.Usuario;
using Microsoft.AspNetCore.Mvc;



namespace Application.Controllers;

public class UsuarioController : Controller
{
    private readonly UCcrearCuentaUser _crearCuenta;
    private readonly UCRestablecerClave _restablecerClave;
    private readonly UCCambiarClave _cambiarClave;
    private readonly UCListarRol _listarRol;
    private readonly UCObtenerDepartamento _obtenerDepartamento;
    private readonly UCObtenerMunicipio _obtenerMunicipio;
    private readonly UCListarUsuario _listarUsuario;

    public UsuarioController(UCcrearCuentaUser crearCuenta,UCRestablecerClave restablecerClave, UCCambiarClave cambiarClave,
    UCListarRol listarRol,UCObtenerDepartamento obtenerDepartamento, UCObtenerMunicipio obtenerMunicipio,UCListarUsuario listUsuario) 
    { 
        _crearCuenta = crearCuenta;
        _restablecerClave = restablecerClave;
        _cambiarClave= cambiarClave;
        _listarRol= listarRol;
        _obtenerDepartamento=obtenerDepartamento;
        _obtenerMunicipio=obtenerMunicipio;
        _listarUsuario = listUsuario;
        _cambiarClave = cambiarClave;
        
    }

    #region Vistas
    //Vista de Creacion de Cuenta
    public IActionResult CrearCuenta()
    {
        return View();
    }

    //Vista de Restablecimiento o recuperacion de clave
    public IActionResult RestablecerClave()
    {
        return View();
    }

    //Vista de Cambio de clave
    public IActionResult CambiarClave()
    {
        return View();
    }
    #endregion


    #region RecursosDeMuestraEnElFormulario

    [HttpGet]
    public async Task<IActionResult> ListarDepartamento()
    {
        List<DMDepartamento> lista = await _obtenerDepartamento.ListaDepartament();
        return Json(new { listaDepartamento = lista });
    }


    [HttpPost]
    public async Task<IActionResult> ObtenerMunicipio(int idDepartamento)
    {
        List<DMMunicipio> lista = await _obtenerMunicipio.ObtenerMunicipi(idDepartamento);
        return Json(new { listaMunicipo = lista });
    }


    [HttpGet]
    public async Task<IActionResult> ListarRol()
    {
        List<DMRol> lista=await _listarRol.ListarTipoRol();
        return Json(new { listaRol= lista });
    }
    #endregion



    #region MetodosHttp
    //metodo de crear cuenta de Usuario
    [HttpPost]
    public async Task<IActionResult> CrearCuenta([FromBody] DMUsuario user)
    {
        int resultado;
        resultado = await _crearCuenta.CrearCuenta(user);
        if (resultado > 0)
        {
            return Json(new { success = true, message = "Cuenta creada exitosamente" });
        }
        else
        {
            return Json(new { success = false, message = "No se pudo crear la cuenta." });
        }
    }


    //metodo para recuperar clave de usuario
    [HttpPost]
    public async Task<IActionResult> RestablecerClave(string correo)
    {
        var listaUsuario = await _listarUsuario.ListarUsuario();
        var usuarioPorcorreo=listaUsuario.Where(item=>item.Correo==correo).FirstOrDefault();

        if (usuarioPorcorreo == null)
        {
            TempData["Error"]="Correo Incorrecto";
            return View();
        }
        else
        {
            bool resultado = await _restablecerClave.RestablecerClaveUser(usuarioPorcorreo.IdUsuario, correo);
            //exito
            if (resultado)
            {
                TempData["SuccessMessage"] = "Clave Recuperada De Forma Exitosa";
                return View();
            }
            else
            {
                TempData["ErrorRecuperacion"] = "No Se Pudo recuperar la Clave";
                return View();
            }
        }
    }


    //metodo para cambiar clave una vez restablecida desde el correo
    [HttpPost]
    public async Task<IActionResult> CambiarClave(string idUsuario, string claveActual, string nuevaClave, string confirmarClave)
    {
        var listaUsuario = await _listarUsuario.ListarUsuario();
        var usuarioPorID = listaUsuario.Where(item => item.IdUsuario == int.Parse(idUsuario)).FirstOrDefault();

        if(usuarioPorID.Clave_hash != BCrypt.Net.BCrypt.HashPassword(claveActual))
        {
            TempData["IdUsuario"] =idUsuario;
            ViewData["vclave"] = "";
            ViewBag.Error = "La Clave actual no es correcta";
            return View();
        }else if(nuevaClave != confirmarClave)
        {

            TempData["IdUsuario"] = idUsuario;
            ViewData["vclave"] = claveActual;
            ViewBag.Error = "Las Claves no Coinciden";
            return View();
        }
        ViewData["vclave"] = "";
        bool respuesta =await _cambiarClave.CambiarClaveUser(Convert.ToInt32(idUsuario), nuevaClave);
        if (respuesta)
        {
            TempData["SuccessMessage"] = "Clave Cambiada Exitosamente";
            return View(); 
        }
        else
        {
            TempData["IdUsuario"] = idUsuario;
            return View();
        }
    }
    #endregion
}
