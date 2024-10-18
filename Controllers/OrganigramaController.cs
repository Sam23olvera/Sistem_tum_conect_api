using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConectDB.Controllers
{
    public class OrganigramaController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        DataApi data = new DataApi();
        UsuarioModel model = new UsuarioModel();
        ConectMenuUser menu = new ConectMenuUser();
        Error msj = new Error();
        OrganigramaDB db = new OrganigramaDB();
        Organizacion orga = new Organizacion();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(int cveEmp, string XT)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
                ViewData["UsuarioModel"] = model;
                model.Token = XT;
                orga = db.Mapaorganizacion();
                return View("Index", orga);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
    }
}
