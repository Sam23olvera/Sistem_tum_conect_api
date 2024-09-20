using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.Controllers
{
    public class PreTicketController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        ConectMenuUser menu = new ConectMenuUser();
        UsuarioModel model = new UsuarioModel();
        PreticketDB PreticketDB = new PreticketDB();
        PreTicketMod Premod = new PreTicketMod();
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
                ViewData["Title"] = "Pre Ticket";
                Premod = PreticketDB.ConsultaPreticket(model.Data[0].EmpS[0].cveEmp,null, 0);
                return View("Index", Premod);
            }
            catch (Exception e)
            {
                error.status = 400;
                error.message = e.Message;
                return View("Error", error);
            }
        }
        [HttpPost, HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Buscar(int cveEmp, string XT, DateTime FechCreTick, int Tipotick)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Pre Ticket";
                Premod = PreticketDB.ConsultaPreticket(model.Data[0].EmpS[0].cveEmp, FechCreTick.ToString("yyyy-MM-dd"), Tipotick);
                if (Premod.Errores[0].status == 400)
                {
                    TempData["Mensaje"] = Premod.Errores[0].message;
                }
                return View("Index", Premod);
            }
            catch (Exception e)
            {
                error.status = 400;
                error.message = e.Message;
                return View("Error", error);
            }
        }
        [HttpPost, HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Cretick(int cveEmp, string XT, string SelClvTick)
        {
            try 
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Pre Ticket";
                if (!string.IsNullOrEmpty(SelClvTick))
                {
                    string[] clavetick = SelClvTick.Split(',');
                    Premod = PreticketDB.CreaTick(clavetick, model.Data[0].EmpS[0].cveEmp, 2, model.Data[0].idus);
                    if (Premod.Errores[0].status == 200)
                    {
                        TempData["guardado"] = Premod.Errores[0].message;
                    }
                    else
                    {
                        TempData["Mensaje"] = Premod.Errores[0].message;
                    }
                }
                else {
                    TempData["Mensaje"] = "Seleccione un Pretickt";
                    return RedirectToAction("Index", new {  cveEmp,XT });
                }
                return View("Index", Premod);
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
