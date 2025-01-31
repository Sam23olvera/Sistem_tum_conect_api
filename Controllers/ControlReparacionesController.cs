using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel.DataAnnotations;

namespace ConectDB.Controllers
{
    public class ControlReparacionesController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        private readonly DataApi data = new DataApi();
        private readonly ConexionApiControlReparaciones cone = new ConexionApiControlReparaciones();
        
        ControlFalla? controlFal = new ControlFalla();
        UsuarioModel? model = new UsuarioModel();
        private readonly Error msj = new Error();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                controlFal = cone.PrimerCarga_sin_catlog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ViewData["Title"] = "Inicio";
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
