using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.Controllers
{
    public class FlotaSepomexController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        DataApi data = new DataApi();
        ConectMenuUser menu = new ConectMenuUser();
        UsuarioModel model = new UsuarioModel();
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                return RedirectToAction("Index", "Loging");

            string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
            string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

            model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
            model.Token = XT;
            ViewData["UsuarioModel"] = model;            
            //JObject JHR = JObject.Parse("{local:[{name:\"TUM TRANSPORTISTAS UNIDOS MEXICANOS CUAUTITLAN IZCALLI\", lat: 19.61747213015414, lng: -99.22406370789754 }, { name: \"TUM TRANSPORTISTAS UNIDOS MEXICANOS QUERÉTARO\", lat: 20.570928711236398, lng: -100.30288772919846 }, { name: \"TUM TRANSPORTISTAS UNIDOS MEXICANOS GUADALAJARA\", lat: 20.56577818262262, lng: -103.26878318164584 },{ name: \"TUM TRANSPORTISTAS UNIDOS MEXICANOS AGUASCALIENTES\", lat: 21.76029500758191, lng: -102.27903498636567 }]}");
            JObject JHR = JObject.Parse("{local:[{name:\"lola\", lat: 19.61747213015414, lng: -99.22406370789754 }]}");
            TempData["ffa"] = JHR;

            return View("Index");
        }
    }
}
