using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Printing;
using static ConectDB.Models.LogUser;
using ConectDB.Excel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConectDB.Controllers
{
    public class ConsultaporOperacionController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        private const int pageSize = 10;
        UsuarioModel? model = new UsuarioModel();
        Error msj = new Error();
        ConsulTipoOpera catop = new ConsulTipoOpera();
        ConsultasOperar Consulta = new ConsultasOperar();
        Class_Expo_Excel _Expo_Excel = new Class_Expo_Excel();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Consulta de Operacion";
                catop = Consulta.PrimeraCarga(model.Data[0].EmpS[0].cveEmp);
                return View("Index", catop);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [HttpPost,HttpGet]
        public IActionResult Busca(int NumTicket, DateTime FehInicio, DateTime FehFin,int ClaveUnidadNegocio, int ClaveTipoOperacion,int clvEstatus, bool Excel, int cveEmp, string XT, int pagina) 
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                //model = JsonConvert.DeserializeObject <UsuarioModel>(menu.ToString());
                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Consulta de Operacion";
                if (FehInicio == null || FehInicio == DateTime.MinValue)
                {
                    FehInicio = DateTime.Now;
                }
                else if (FehFin == null || FehFin == DateTime.MinValue)
                {
                    FehFin = DateTime.Now;
                }
                if (NumTicket != 0) 
                {
                    catop = Consulta.SegundaCarga(model.Data[0].EmpS[0].cveEmp, NumTicket, null, null, 0, 0,0, pagina, pageSize, Excel);
                }
                else {
                    catop = Consulta.SegundaCarga(model.Data[0].EmpS[0].cveEmp, NumTicket, FehInicio.ToString("yyyy-MM-dd HH:mm:ss"), FehFin.ToString("yyyy-MM-dd HH:mm:ss"), ClaveUnidadNegocio, ClaveTipoOperacion, clvEstatus, pagina, pageSize, Excel);
                }
                if (catop.CSxTipoOeracion.Count == 0)
                {
                    TempData["Mensaje"] = "No se encuentran datos";
                }
                else
                {
                    if (Excel == true)
                    {
                        FileResult file = _Expo_Excel.Excel_exportOper(catop, "Expo_" + DateTime.Now.ToString("yyyy-MM-dd"));
                        return file;
                    }
                    else 
                    {
                        ViewBag.TotalPages = (int)Math.Ceiling((double)catop.TotalSolicitudes / pageSize);
                        ViewBag.CurrentPage = pagina;
                    }
                }
                TempData["ClaveUniNegocio"] = ClaveUnidadNegocio;
                TempData["ClaveTipoOperacion"] = ClaveTipoOperacion;
                TempData["clvEstatus"] = clvEstatus;
                TempData["FehInicio"] = FehInicio;
                TempData["FehFin"] = FehFin;
                return View("Index", catop);
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
