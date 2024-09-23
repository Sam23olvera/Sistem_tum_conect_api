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
                CFVPreticketDB = Premod.ConsultaPreticket(DateTime.Now.ToString("yyyy-MM-dd HH:mm"), null, cveEmp);
                if (CFVPreticketDB.Errors[0].status == 400)
                {
                    TempData["Mensaje"] = CFVPreticketDB.Errors[0].message;
                }
                else
                {
                    TempData["guardado"] = CFVPreticketDB.Errors[0].message;
                }
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
        public ActionResult buscarcfvpretick(int cveEmp, DateTime FechCrePreTickIn, DateTime FechCrePreTickFin, string XT)
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
                CFVPreticketDB = Premod.ConsultaPreticket(FechCrePreTickIn.ToString("yyyy-MM-dd HH:mm:ss"), FechCrePreTickFin.ToString("yyyy-MM-dd HH:mm:ss"), cveEmp);
                if (CFVPreticketDB.Errors[0].status == 400)
                {
                    TempData["Mensaje"] = CFVPreticketDB.Errors[0].message;
                }
                else 
                {
                    TempData["guardado"] = CFVPreticketDB.Errors[0].message;
                }
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
