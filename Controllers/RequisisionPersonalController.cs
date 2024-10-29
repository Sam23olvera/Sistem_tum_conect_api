using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

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
        public IActionResult Guardar(int cveEmp, string XT, RequierePersonas RQP)
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
                if (!ModelState.IsValid)
                {
                    if (RQP.selnombrePuesto == 0) 
                    {
                        TempData["Mensaje"] = "selecione un puesto";
                    }
                    if (RQP.localidad == 0) 
                    {
                        TempData["Mensaje"] = "selecione un localidad";
                    }
                    if (RQP.unidadNegocio == 0) 
                    {
                        TempData["Mensaje"] = "selecione un Unida de Negocio";
                    }
                    if (RQP.seledepa == 0) 
                    {
                        TempData["Mensaje"] = "selecione un departamento";
                    }
                    if (RQP.subdept == 0) 
                    {
                        TempData["Mensaje"] = "selecione un subdepartamento";
                    }
                        return RedirectToAction("Index", new { cveEmp, XT });
                }
                JObject JRespuesta = dbRePer.guardarReqPerson(RQP);
                JArray data = JRespuesta["data"] as JArray;
                if (Convert.ToInt16(JRespuesta["status"]) == 200)
                {
                    TempData["guardado"] = JRespuesta["message"].ToString();
                }
                else
                {
                    TempData["Mensaje"] = JRespuesta["message"].ToString();
                }
                return View("index");
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
