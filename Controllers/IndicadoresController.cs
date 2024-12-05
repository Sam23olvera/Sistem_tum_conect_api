using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ConectDB.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        private readonly DataApi data = new DataApi();
        private readonly ConexionApiControlReparaciones con = new ConexionApiControlReparaciones();
        ControlFalla? controlFal = new ControlFalla();
        UsuarioModel? model = new UsuarioModel();
        Error msj = new Error();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Indicadores";
                controlFal = con.PrimerCarga_sin_catlog(0, model.Data[0]?.EmpS[0].cveEmp.ToString(), model.Data[0].idus.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), 0, idsub);
                return View("Index", controlFal);
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
