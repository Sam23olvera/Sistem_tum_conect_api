










































































































































































































using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.Controllers
{
    public class CFVPretickController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        ConectMenuUser menu = new ConectMenuUser();
        UsuarioModel model = new UsuarioModel();
        CFVPretickDatum CFVPreticketDB = new CFVPretickDatum();
        CFVPretikDB Premod = new CFVPretikDB();
        Error error = new Error();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Pre-Ticket";
                CFVPreticketDB = Premod.ConsultaPreticket(DateTime.Now, cveEmp);
                return View("Index", CFVPreticketDB);
            }
            catch (Exception e)
            {
                error.status = 400;
                error.message = e.Message;
                return View("Error", error);
            }
        }
        [HttpPost,HttpGet]
        public ActionResult buscarcfvpretick(int cveEmp, DateTime FechCrePreTick, string XT)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Pre-Ticket";
                CFVPreticketDB = Premod.ConsultaPreticket(FechCrePreTick, cveEmp);
                return View("Index", CFVPreticketDB);
            }
            catch (Exception e)
            {
                error.status = 400;
                error.message = e.Message;
                return View("Error", error);
            }
        }
    }
}
