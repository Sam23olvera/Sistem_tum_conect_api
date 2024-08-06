using ConectDB.DB;
using ConectDB.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ConectDB.Models.LogUser;

namespace ConectDB.Controllers
{
    public class LogingController : Controller
    {
        DataApi data = new DataApi();
        LogUser jsdatos = new LogUser();
        private string url2 = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/logau";
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("usuario");
            HttpContext.Response.Cookies.Delete("contra");
            return View("Index");
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult logeo(Filter log)
        {
            string usuarioCifrado = UrlEncryptor.EncryptUrl(log.usr);
            string contraseñaCifrada = UrlEncryptor.EncryptUrl(log.pwd);
                        
            if (log.usr == null || log.pwd == null)
            {
                return View("Error");
            }
            else
            {
                JObject jsdatos = JObject.Parse("{\"data\": {\"bdCc\": 1,\"bdSch\": \"dbo\",\"bdSp\": \"SPQRY_User\"},\"filter\": {\"usr\": \"" + log.usr + "\",\"pwd\": \"" + log.pwd + "\" }}");
                var datos = JsonConvert.DeserializeObject<UsuarioModel>(data.HttpWebRequest("POST", url, jsdatos));
                if (datos == null)
                {
                    return View("Error");
                }
                else
                {
                    HttpContext.Response.Cookies.Append("usuario", usuarioCifrado);
                    HttpContext.Response.Cookies.Append("contra", contraseñaCifrada);
                    HttpContext.Session.SetString("UsuarioModel", JsonConvert.SerializeObject(datos));
                    return View(datos);
                }
            }
        }
        //public IActionResult Acceder(int CVEM, string US, string XT, string Tok)
        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Acceder(int CVEM, string Tok)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]))
            {
                return RedirectToAction("Index", "Loging");
            }
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
            {
                return RedirectToAction("Index", "Loging");
            }
            string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
            string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

            jsdatos = new LogUser { data = new Data { bdCc = 1, bdSch = "dbo", bdSp = "SPQRY_EmpUser" }, filter = new Filter { usr = desusuario, pwd = descontraseña, idempresa = CVEM } };
            var datos = data.HttpWebRequestTokenLog("POST", url2, JsonConvert.SerializeObject(jsdatos), Tok);
            if (datos == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                var model = JsonConvert.DeserializeObject<UsuarioModel>(datos);
                model.Token = Tok;
                ViewData["UsuarioModel"] = model;
                return View("Acceder", model);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("usuario", new CookieOptions { Path = "/" });
            HttpContext.Response.Cookies.Delete("contra", new CookieOptions { Path = "/" });
            TempData["Mensaje"] = "Se Cerro la Sesion";
            return RedirectToAction("Index");
        }

    }
}