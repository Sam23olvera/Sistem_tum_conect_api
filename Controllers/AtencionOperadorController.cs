using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Security.Policy;

namespace ConectDB.Controllers
{
    public class AtencionOperadorController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        UsuarioModel? model = new UsuarioModel();
        Error msj = new Error();
        AtenOperador AtenOperador = new AtenOperador();
        AteoperConsultas consul = new AteoperConsultas();
        public IActionResult Index(int cveEmp, string XT, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Inicio";
                AtenOperador = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                return View("Index", AtenOperador);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [HttpPost]
        public IActionResult Buscar(int cveEmp, string XT, int idsub, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Inicio";
                AtenOperador = consul.Buscar(model.Data[0].EmpS[0].cveEmp.ToString(), FechaInicio.ToString("yyyy-MM-dd HH:mm:ss"), FechaFin.ToString("yyyy-MM-dd HH:mm:ss"));
                if (AtenOperador.Erroress[0].status != 200)
                {
                    TempData["Mensaje"] = AtenOperador.Erroress[0].status + AtenOperador.Erroress[0].message;
                    return RedirectToAction("Index", new { cveEmp, XT, idsub });
                }
                TempData["guardado"] = AtenOperador.Erroress[0].status + AtenOperador.Erroress[0].message;
                return View("Index", AtenOperador);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }

        }
        [HttpPost]
        public IActionResult RegistrarAtencion(int cveEmp, string XT, int idsub, AtenOperador registraroper, int ClaveUnidad, int selDolly, int Remolques)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), cveEmp, url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                ViewData["Title"] = "Inicio";
                if (!ModelState.IsValid)
                {
                    if (registraroper.ClaveTipoTicket == 0)
                    {
                        TempData["Mensaje"] = "Debes de Selecionar un Tipo de Ticket";
                        registraroper = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                        return View("Index", registraroper);
                    }
                    if (registraroper.ClaveOperador == 0)
                    {
                        TempData["Mensaje"] = "Debes de Selecionar un Operador";
                        registraroper = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                        return View("Index", registraroper);
                    }
                    else if (registraroper.ClaveTipoTicket == 1)
                    {
                        if (ClaveUnidad == 0)
                        {
                            //regresar a selecionar una unidad
                            TempData["Mensaje"] = "Debes de Selecionar una Unidad";
                            registraroper = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                            return View("Index", registraroper);
                        }
                        else
                        {
                            AtenOperador = consul.Guardar(registraroper,ClaveUnidad);
                        }
                    }
                    else if (registraroper.ClaveTipoTicket == 2)
                    {
                        if (Remolques == 0)
                        {
                            //regresar a selecionar un remolque
                            TempData["Mensaje"] = "Debes de Selecionar un Remolque";
                            registraroper = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                            return View("Index", registraroper);
                        }
                        else
                        {
                            AtenOperador = consul.Guardar(registraroper, Remolques);
                            AtenOperador = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                        }
                    }
                    else if (registraroper.ClaveTipoTicket == 3)
                    {
                        if (selDolly == 0)
                        {
                            //regresar a selecionar un selDolly
                            TempData["Mensaje"] = "Debes de Selecionar un Dolly";
                            registraroper = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                            return View("Index", registraroper);
                        }
                        else
                        {
                            AtenOperador = consul.Guardar(registraroper,selDolly);
                            AtenOperador = consul.Coatalgos(model.Data[0].EmpS[0].cveEmp.ToString());
                        }
                    }
                }
                if (AtenOperador.Erroress[0].status != 200)
                {
                    TempData["Mensaje"] = AtenOperador.Erroress[0].status + AtenOperador.Erroress[0].message;
                    return RedirectToAction("Index", new { cveEmp, XT, idsub });
                }
                TempData["guardado"] = AtenOperador.Erroress[0].status + AtenOperador.Erroress[0].message;
                return View("Index", AtenOperador);
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
