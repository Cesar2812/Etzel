using LayerUseCase.Usuario;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Controllers
{
    public class LoginAccesoController : Controller
    {
        private readonly UCListarUsuario _listarUsuario;

        public LoginAccesoController(UCListarUsuario listarUsuario)
        {
            _listarUsuario = listarUsuario;
        }


        #region VistaInicioDeSesion
        //vista de Inicio de Sesion
        public IActionResult InicioDeSesion()
        {
            return View();
        }
        #endregion


        //metodo de inicio de sesion
        [HttpPost]
        public async Task<IActionResult> InicioDeSesion(string correo,string clave)
        {
            var listaUsuario = await _listarUsuario.ListarUsuario();
            var usuarioEncontrado = listaUsuario.Where(item => item.Correo ==correo && item.Clave_hash == BCrypt.Net.BCrypt.HashPassword(clave)).FirstOrDefault();

            if(usuarioEncontrado == null)
            {
                ViewBag.Error = "Correo o Clave incorrectas";
                return View();
            }
            else
            {

                if (usuarioEncontrado.Restablecer)
                {
                    TempData["IdUsuario"] = usuarioEncontrado.IdUsuario;
                    return RedirectToAction("CambiarClave", "Usuario");
                }
                else
                {

                    //creando Clains para parametrizar la autenticacion para pasarlos a la Cookie 
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuarioEncontrado.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Name,usuarioEncontrado.NombrePersona),
                        new Claim("UserName",usuarioEncontrado.Correo),
                        new Claim(ClaimTypes.Role, usuarioEncontrado.objRol.DescripcionRol)
                    };

                    //creando la autenticacion por cookie en base a los roles de cada usuario
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //creando la cookie al iniciar sesion dentro de la app
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");

                }
            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            //elimando cookie creada al cerrar sesion
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }
    }
}
