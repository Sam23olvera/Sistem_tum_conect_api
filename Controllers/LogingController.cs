using ConectDB.DB;
using ConectDB.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static ConectDB.Models.LogUser;

namespace ConectDB.Controllers
{
    public class LogingController : Controller
    {
        DataApi data = new DataApi();
        LogUser logdata = new LogUser();
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
                logdata = new LogUser
                {
                    data = new Data { bdCc = 1, bdSch = "dbo", bdSp = "SPQRY_User" },
                    filter = log
                };
                JObject jsdatos = JObject.Parse(JsonConvert.SerializeObject(logdata));
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

                    // Guardar datos en TempData para usarlos en la redirección
                    TempData["UsuarioModel"] = JsonConvert.SerializeObject(datos);
                    // Redirigir a SeleccionEmpresa (GET)
                    return RedirectToAction("SeleccionEmpresa");
                    //return View("logeo", datos);
                }
            }
        }
        [HttpGet]
        public IActionResult SeleccionEmpresa()
        {
            // Recuperar datos desde TempData
            if (TempData["UsuarioModel"] == null)
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Cookies.Delete("usuario", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
                HttpContext.Response.Cookies.Delete("contra", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
                TempData["Mensaje"] = "Se Cerro la Sesion";
                return RedirectToAction("Index");
            }

            var model = JsonConvert.DeserializeObject<UsuarioModel>(TempData["UsuarioModel"].ToString());

            // Pasar datos a la vista
            return View("logeo", model);
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Acceder(int CVEM, string Tok)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                return RedirectToAction("Index", "Loging");

            string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
            string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

            logdata = new LogUser { data = new Data { bdCc = 1, bdSch = "dbo", bdSp = "SPQRY_EmpUser" }, filter = new Filter { usr = desusuario, pwd = descontraseña, idempresa = CVEM } };
            var datos = data.HttpWebRequestTokenLog("POST", url2, JsonConvert.SerializeObject(logdata), Tok);
            if (datos == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                var model = JsonConvert.DeserializeObject<UsuarioModel>(datos);
                model.Token = Tok;
                //ViewData["UsuarioModel"] = model;
                TempData["UsuarioModel"] = JsonConvert.SerializeObject(model);
                //return View("Acceder", model);
                return RedirectToAction("AccederRedirect", new { CVEM, XT = Tok });
            }
        }
        public IActionResult AccederRedirect(int CVEM, string XT)
        {
            if (TempData["UsuarioModel"] == null)
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Cookies.Delete("usuario", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
                HttpContext.Response.Cookies.Delete("contra", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
                TempData["Mensaje"] = "Se Cerro la Sesion";
                return RedirectToAction("Index", "Loging");
            }
            var model = JsonConvert.DeserializeObject<UsuarioModel>(TempData["UsuarioModel"].ToString());
            ViewData["UsuarioModel"] = model;

            return View("Acceder", model);
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("usuario", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
            HttpContext.Response.Cookies.Delete("contra", new CookieOptions { Expires = DateTimeOffset.Now.AddDays(-1), Path = "/" });
            TempData["Mensaje"] = "Se Cerro la Sesion";
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            Error errors = new Error();
            return View("Error_Pag", errors);
        }
    }
}