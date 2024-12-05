using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.Controllers
{
    public class HeadCountController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        DataApi data = new DataApi();
        UsuarioModel model = new UsuarioModel();
        ConectMenuUser menu = new ConectMenuUser();
        Error msj = new Error();
        MovimientosHead headDB = new MovimientosHead();
        CuentaCabezas cabezas = new CuentaCabezas();

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
                ViewData["UsuarioModel"] = model;
                model.Token = XT;
                cabezas = headDB.TraerCabeza();
                return View("Index", cabezas);
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = ex.Message;
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public ActionResult GuardarBaja(int cveEmp, string XT, int editClaveEmpleado, int editClaveHeadCountBaja, int idCausa, DateTime FechaBaja, bool RecContra, string txtObaserva)
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

                JObject json = headDB.MovimientoBaja(editClaveEmpleado, editClaveHeadCountBaja, idCausa, FechaBaja, RecContra, txtObaserva, model.Data[0].idus);
                if (json["status"]?.ToString() != "200")
                {
                    TempData["Mensaje"] = json["status"].ToString() + json["message"].ToString();
                }
                else
                {
                    TempData["guardado"] = json["status"].ToString() + json["message"].ToString();
                }
                return RedirectToAction("Index", new { cveEmp, XT });
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = ex.Message;
                return View("Error", msj);
            }
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public ActionResult GuardarPromocion(int cveEmp, string XT, int editClaveEmpleadoProm, int selectPromo, string txtObaservaProm)
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

                JObject json = headDB.Promociones(editClaveEmpleadoProm, selectPromo, txtObaservaProm, model.Data[0].idus);
                if (json["status"]?.ToString() != "200")
                {
                    TempData["Mensaje"] = json["status"].ToString() + json["message"].ToString();
                }
                else
                {
                    TempData["guardado"] = json["status"].ToString() + json["message"].ToString();
                }
                return RedirectToAction("Index", new { cveEmp, XT });
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