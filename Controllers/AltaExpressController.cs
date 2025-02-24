using ConectDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using ConectDB.DB;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ConectDB.Controllers
{
    public class AltaExpressController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        private readonly Error msj = new Error();
        AltaExpressMovimientos mov = new AltaExpressMovimientos();
        AltaExpressModel Model = new AltaExpressModel();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            try
            {
                Model = new AltaExpressModel();
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
                string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

                var model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                TempData["status"] = 400;
                Model = mov.Catalogos(cveEmp);
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + ex.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public ActionResult Guardar(int cveEmp, string XT, AltaExpressModel guar)
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
                Model = mov.Catalogos(cveEmp);
                guar.TBCAT_TipoOp = Model.TBCAT_TipoOp;
                guar.TBCAT_TipoTrabajador = Model.TBCAT_TipoTrabajador;
                guar.TBCAT_Escolaridad = Model.TBCAT_Escolaridad;
                guar.TBCAT_TipoLicencia = Model.TBCAT_TipoLicencia;
                guar.TBCAT_ZonaTrabajo = Model.TBCAT_ZonaTrabajo;
                guar.TBCAT_PuestosOperativos = Model.TBCAT_PuestosOperativos;
                guar.TBCAT_DoctoRecibidos = Model.TBCAT_DoctoRecibidos;
                guar.TBCAT_Bancos = Model.TBCAT_Bancos;
                guar.TBCAT_EsquemaPago = Model.TBCAT_EsquemaPago;
                guar.TBCAT_Pais = Model.TBCAT_Pais;
                guar.TBCAT_Estado = Model.TBCAT_Estado;
                guar.TBCAT_OriginarioDe = Model.TBCAT_OriginarioDe;
                guar.TBCAT_EstadoCivil = Model.TBCAT_EstadoCivil;
                if (guar.CveTipoEmp == 0)
                {
                    ModelState.AddModelError("CveTipoEmp", "Debes Seleccionar un Tipo de Trabajador");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.SelPais == 0)
                {
                    ModelState.AddModelError("SelPais", "Debes Seleccionar un Pais");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.selEstado == 0)
                {
                    ModelState.AddModelError("selEstado", "Debes Seleccionar un Estado");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.seleMuni == 0)
                {
                    ModelState.AddModelError("seleMuni", "Debes Seleccionar un Municipio");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.Colonia == 0)
                {
                    ModelState.AddModelError("Colonia", "Debes Seleccionar la Colonia");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.Escol == 0)
                {
                    ModelState.AddModelError("Escol", "Debes Seleccionar la Escolaridad");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.originario == 0)
                {
                    ModelState.AddModelError("originario", "Debes de selecionar un Lugar de Nacimiento");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.SeleLic == 0)
                {
                    ModelState.AddModelError("SeleLic", "Debes Seleccionar un Tipo de Licencia");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.TipOpera == 0)
                {
                    ModelState.AddModelError("TipOpera", "Debes Seleccionar un Tipo de Operacion");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.ZonTra == 0)
                {
                    ModelState.AddModelError("ZonTra", "Debes Seleccionar una Zona de Trabajo");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.Banc == 0)
                {
                    ModelState.AddModelError("Banc", "Debes Seleccionar un Banco");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.selePues == 0)
                {
                    ModelState.AddModelError("selePues", "Debe seleccionar un Puesto");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (guar.EdoCivil == 0)
                {
                    ModelState.AddModelError("EdoCivil", "Debe seleccionar un de los Estado Civil");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                if (!(guar.RangoMedio || guar.Thorton || guar.Rabon || guar.Camioneta || guar.TractoSenci || guar.VHlig || guar.TracFull))
                {
                    ModelState.AddModelError("RangoMedio", "Debes seleccionar al menos un tipo de vehículo.");
                    TempData["status"] = 400;
                    return View("Index", guar);
                }
                var alta = "";
                if (!ModelState.IsValid)
                {
                    guar = mov.Guardar(guar);
                }
                if (guar.Errors[0].status == 200)
                {
                    TempData["guardado"] = guar.Errors[0].message;
                    TempData["status"] = guar.Errors[0].status;
                    string[] numalta = guar.Errors[0].message.Split("|");
                    alta = numalta[1];
                    TempData["alta"] = alta;
                    //return View("Index", guar);
                    return RedirectToAction("Imprime", new { cveEmp, XT, alta });
                }
                else
                {
                    TempData["Mensaje"] = guar.Errors[0].message;
                    return View("Index", guar);
                }
            }
            catch (Exception ex)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + ex.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Imprime(int cveEmp, string XT, string alta)
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
                Model = mov.Impresion(alta);
                TempData["status"] = Model.Errors[0].status;
                TempData["guardado"] = Model.Errors[0].message;
                return View("Imprime",Model);
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
