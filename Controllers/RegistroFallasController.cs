using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using System;
using static ConectDB.Models.LogUser;

namespace ConectDB.Controllers
{
    public class RegistroFallasController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly Error msj = new Error();
        private readonly ConectRegistraFalla con = new ConectRegistraFalla();
        private readonly ConectMenuUser menu = new ConectMenuUser();
            

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");
                
                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                var model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;

                var oLista = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                return View("Index", oLista);
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + ex.Message.ToString();
                return View("Error", msj);
            }
        }

        [HttpPost, HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Guardar(string XT, ModelFallas fallas, List<IFormFile> Files)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                var model = menu.RegresMenu(desusuario, descontraseña, fallas.cveEmpUser, url, XT);
                ViewData["UsuarioModel"] = model;
                model.Token = XT;
                if (fallas.selAccion == 0)
                {
                    TempData["Mensaje"] = "Debes de Selecionar una Accion";
                    TempData["check"] = fallas.inCheckViaje;
                    fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                    return View("Index", fallas);
                }
                if (fallas.selTipCarga == 0)
                {
                    TempData["Mensaje"] = "Debes de Selecionar una Tipo Carga";
                    TempData["check"] = fallas.inCheckViaje;
                    fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                    return View("Index", fallas);
                }
                if (fallas.seleuni == 0)
                {
                    TempData["Mensaje"] = "Debes de Selecionar una Unidad";
                    TempData["check"] = fallas.inCheckViaje;
                    fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                    return View("Index", fallas);
                }
                if (fallas.inCheckViaje != true)
                {
                    if (fallas.Opera == 0)
                    {
                        TempData["Mensaje"] = "Debes de Selecionar un operador";
                        TempData["check"] = fallas.inCheckViaje;
                        fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                        return View("Index", fallas);
                    }
                    if (string.IsNullOrEmpty(fallas.Ruta))
                    {
                        TempData["Mensaje"] = "Agrega una Ruta";
                        TempData["check"] = fallas.inCheckViaje;
                        fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                        return View("Index", fallas);
                    }
                    if (fallas.SelectOperacion == 0)
                    {
                        TempData["Mensaje"] = "Debes de Selecionar una Operacion";
                        TempData["check"] = fallas.inCheckViaje;
                        fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                        return View("Index", fallas);
                    }
                }
                if (string.IsNullOrEmpty(fallas.clavesFalAndComen))
                {
                    TempData["Mensaje"] = "Debes de agregar una falla por lo menos";
                    TempData["check"] = fallas.inCheckViaje;
                    fallas = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
                    return View("Index", fallas);
                }
                fallas = con.Guardar(fallas, model.Data[0].idus);
                if (fallas.Eror[0].status == 200)
                {
                    TempData["guardado"] = fallas.Eror[0].message;
                    TempData["status"] = fallas.Eror[0].status;
                    return RedirectToAction("Index", new { model.Data[0].EmpS[0].cveEmp, XT });
                }
                else
                {
                    TempData["Mensaje"] = fallas.Eror[0].message;
                    TempData["status"] = fallas.Eror[0].status;
                    return RedirectToAction("Index", new { model.Data[0].EmpS[0].cveEmp, XT });
                }
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + ex.Message.ToString();
                return View("Error", msj);
            }
        }

    }
}
