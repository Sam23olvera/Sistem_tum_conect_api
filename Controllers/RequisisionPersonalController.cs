using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConectDB.Controllers
{
    public class RequisisionPersonalController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        DataApi data = new DataApi();
        UsuarioModel model = new UsuarioModel();
        ConectMenuUser menu = new ConectMenuUser();
        Error msj = new Error();
        RequierePersonas RequiPer = new RequierePersonas();
        RequierePersonalDB dbRePer = new RequierePersonalDB();

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
                RequiPer = dbRePer.traerCatalogos(model.Data[0].EmpS[0].cveEmp);
                return View("index", RequiPer);
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = ex.Message;
                return View("Error", msj);
            }
        }
        [HttpPost]
        public IActionResult Guardar(int cveEmp, string XT)
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


                return View("index", RequiPer);
            }
            catch (Exception ex) 
            {
                msj.status = 400;
                msj.message = ex.Message;
                return View("Error", msj);
            }
        }

    }
}
