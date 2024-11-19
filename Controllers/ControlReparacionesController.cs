using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Printing;
using System.Net.Http;
using System.Net.Sockets;

namespace ConectDB.Controllers
{
    public class ControlReparacionesController : Controller
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        private readonly ConectMenuUser menu = new ConectMenuUser();
        private readonly DataApi data = new DataApi();
        //private readonly ConectApiContrRep con = new ConectApiContrRep();
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
                controlFal = cone.PrimerCarga_sin_catlog(0, model.Data[0]?.EmpS[0].cveEmp.ToString(), model.Data[0].idus.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub);
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
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PorAsig(string XT, string cveEmp, DateTime FehTick, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                }
                controlFal = cone.PrimerCarga(1, cveEmp, "", 0, 0, 0, 0, 0, 0,true);
                //controlFal = cone.PrimerCarga(1, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, 0, 0, idsub);
                ViewData["Title"] = "Por Asignar";
                TempData["FehTick"] = FehTick;
                return View("PorAsignar", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet, HttpPost]
        public IActionResult BuscarPorAsig(string XT, string cveEmp, int NumTicket, int ClaveTipoFalla, DateTime? FehTick, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (NumTicket == 0)
                {
                    controlFal = cone.PrimerCarga(1, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick?.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, 0, ClaveTipoFalla, 0, 0, idsub, false);
                    //controlFal = cone.PrimerCarga(1, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick?.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, 0, ClaveTipoFalla, 0, 0, idsub);
                }
                else
                {
                    controlFal = cone.PrimerCarga(1, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, 0, ClaveTipoFalla, 0, 0, idsub, false);
                    //controlFal = cone.PrimerCarga(1, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, 0, ClaveTipoFalla, 0, 0, idsub);
                }
                if (controlFal.status == 200)
                {
                    TempData["Buscar"] = 1;
                    TempData["NumTicket"] = NumTicket;
                    TempData["ClaveTipoFalla"] = ClaveTipoFalla;
                    if (FehTick == null || FehTick == DateTime.MinValue)
                    {
                        FehTick = DateTime.Now;
                    }
                    TempData["FehTick"] = FehTick;
                }
                else
                {
                    if (FehTick == null || FehTick == DateTime.MinValue)
                    {
                        FehTick = DateTime.Now;
                        TempData["FehTick"] = FehTick;
                    }
                    else
                    {
                        TempData["FehTick"] = FehTick;
                    }
                    TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                }
                if (controlFal.status != 200)
                {
                    if (controlFal.Solicitudes.Count != 0)
                    {
                        TempData["Mensaje"] = controlFal.status + ' ' + controlFal.message;
                        return RedirectToAction("PorAsig", new { XT, cveEmp, FehTick, idsub });
                    }
                }
                ViewData["Title"] = "Por Asignar";
                return View("PorAsignar", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult AsignacionTicket(string XT, string cveEmp, string Asigna, string ticket, int idsub, int Diesel, int Grua)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                DateTime FehAsignatiempo = DateTime.Now;
                if (Asigna != "[Selecciona]")
                {
                    controlFal = cone.ModificadorFall(1, 2, model.Data[0].EmpS[0].cveEmp.ToString(), ticket, Asigna, 0, 0, "", "", model.Data[0].idus, FehAsignatiempo.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub, Diesel, Grua, "", "", "", 0, false, "");
                    //controlFal = con.ModificadorFall(1, 2, model.Data[0].EmpS[0].cveEmp.ToString(), ticket, Asigna, 0, 0, "", "", model.Data[0].idus, FehAsignatiempo.ToString("yyyy-MM-dd"), 0, idsub, pagina, pageSize, Diesel, Grua, "", "", "", 0, false);
                    if (controlFal.status == 200)
                    {
                        TempData["guardado"] = controlFal.status + "¡ \r\n" + controlFal.message + "\r\n!";
                        return RedirectToAction("BuscarPorAsig", new { XT, cveEmp, NumTicket = 0, ClaveTipoFalla = 0, FehTick= DateTime.Now , idsub, });
                    }
                    else
                    {
                        TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                        return RedirectToAction("BuscarPorAsig", new { XT, cveEmp, NumTicket = 0, ClaveTipoFalla = 0, FehTick = DateTime.Now, idsub, });
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Debes de Seleccionar un persona";
                    return RedirectToAction("BuscarPorAsig", new { XT, cveEmp, NumTicket = 0, ClaveTipoFalla = 0, FehTick = DateTime.Now, idsub, });

                }
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult Asignacion(int pagina, string XT, string cveEmp, string Buscar, int NumTicket, DateTime FehTick, int UsAsignado, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    TempData["FehTick"] = FehTick;
                }
                if (Convert.ToInt32(Buscar) == 0)
                {
                    //controlFal = con.CargarCat(cveEmp, 2);
                    controlFal = cone.PrimerCarga(2, cveEmp, "", 0, 0, 0, 0, 0, 0, true);
                    //controlFal = cone.PrimerCarga(2, cveEmp, "", 0, 0, 0, 0, 0, idsub);
                    TempData["Buscar"] = 0;
                    if (controlFal.status == 200)
                    {
                        ViewData["Title"] = "Asignados";

                    }
                    else if (controlFal.Solicitudes.Count != 0)
                    {
                        msj.status = controlFal.status;
                        msj.message = controlFal.message;
                    }
                }
                else
                {
                    return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado, NumTicket, FehTick, pagina, idsub });
                }
                return View("Asignados", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet, HttpPost]
        public IActionResult BuscarAsignados(string XT, string cveEmp, int UsAsignado, int NumTicket, DateTime FehTick, int pagina, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (NumTicket == 0)
                {
                    //controlFal = con.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, 0, 0, model.Data[0].idus, UsAsignado, idsub, pagina, pageSize);
                    controlFal = cone.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, 0, 0, 0, 0, idsub, false);
                    //controlFal = cone.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, 0, 0, 0, 0, idsub);
                }
                else
                {
                    //controlFal = con.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, 0, 0, model.Data[0].idus, UsAsignado, idsub, pagina, pageSize);
                    controlFal = cone.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, 0, 0, model.Data[0].idus, UsAsignado, idsub, false);
                    //controlFal = cone.PrimerCarga(2, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, 0, 0, model.Data[0].idus, UsAsignado, idsub);
                }
                if (controlFal.status != 200)
                {
                    TempData["Mensaje"] = "¡" + controlFal.status + " " + controlFal.message + "!";
                    if (controlFal.Solicitudes.Count != 0)
                    {
                        msj.status = controlFal.status;
                        msj.message = controlFal.message;
                        return View("Error", msj);
                    }
                }
                TempData["Buscar"] = 1;
                TempData["UsAsignado"] = UsAsignado;
                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick.ToString("yyyy-MM-dd HH:mm:ss");
                }
                TempData["FehTick"] = FehTick;
                TempData["NumTicket"] = NumTicket;
                ViewData["Title"] = "Asignados";
                return View("Asignados", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult AsigTiempApoyClasif(string XT, string cveEmp, DateTime? TiempAsig, DateTime? TiemEta, int Apooyo_Asigna, int Clasif_Asigna, int NumTicket, int pagina, int idsub, int CheckDisel, int CheckGrua, string Dot, string Marca, string Medida, int Posis)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                TempData["FehTick"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (Apooyo_Asigna == 0)
                {
                    TempData["Mensaje"] = "¡Seleccione un Apoyo!";
                    return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                }
                //la fecha de asignacion nula y mayor
                if (!TiempAsig.HasValue || TiempAsig <= DateTime.Now)
                {
                    TempData["Mensaje"] = "¡La fecha de asignación no puede ser nula o anterior.!";
                    return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                }
                //no debe ser menor a hoy y no puede ser nula 
                if (!TiemEta.HasValue || TiemEta <= DateTime.Now)
                {
                    TempData["Mensaje"] = "¡La fecha del eta no puede ser nula o anterior.!";
                    return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                }
                else
                {
                    if (Clasif_Asigna != 2)
                    {
                        Dot = "";
                        Marca = "";
                        Medida = "";
                        Posis = 0;
                    }
                    if (Clasif_Asigna == 2)
                    {
                        if (string.IsNullOrEmpty(Dot))
                        {
                            TempData["Mensaje"] = "¡Debes de llenar el Dot!";
                            return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                        }
                        else if (string.IsNullOrEmpty(Marca))
                        {
                            TempData["Mensaje"] = "¡Debes de llenar la Marca!";
                            return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                        }
                        else if (string.IsNullOrEmpty(Medida))
                        {
                            TempData["Mensaje"] = "¡Debes de llenar el Medida!";
                            return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                        }
                        else if (Posis == 0)
                        {
                            TempData["Mensaje"] = "¡Debes de tener una Posición!";
                            return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                        }
                    }
                    controlFal = cone.ModificadorFall(2, 4, model.Data?[0].EmpS?[0].cveEmp.ToString(), NumTicket.ToString(), "0", Convert.ToInt32(Apooyo_Asigna), Clasif_Asigna, TiempAsig?.ToString("yyyy-MM-dd HH:mm"), "", model.Data[0].idus, TiempAsig?.ToString("yyyy-MM-dd HH:mm"), 0, idsub, CheckDisel, CheckGrua, Dot, Marca, Medida, Posis, false, TiemEta?.ToString("yyyy-MM-dd HH:mm"));
                    //controlFal = con.ModificadorFall2(2, 4, model.Data?[0].EmpS?[0].cveEmp.ToString(), NumTicket.ToString(), 0.ToString(), Convert.ToInt32(Apooyo_Asigna), Clasif_Asigna, TiempAsig?.ToString("yyyy-MM-dd HH:mm"), "", model.Data[0].idus, TiempAsig?.ToString("yyyy-MM-dd HH:mm"), 0, idsub, pagina, pageSize, CheckDisel, CheckGrua, Dot, Marca, Medida, Posis, false, TiemEta?.ToString("yyyy-MM-dd HH:mm"));
                    if (controlFal.status == 200)
                    {
                        TempData["FehTick"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        TempData["guardado"] = "¡Se Guardado Correctamenre!" + controlFal.status + " " + controlFal.message;
                        return RedirectToAction("BuscarAsignados", new { XT, cveEmp, UsAsignado = 0, NumTicket = 0, FehTick = DateTime.Now, pagina, idsub });
                    }
                    else
                    {
                        TempData["Mensaje"] = "¡Error de guardado! " + controlFal.status + " " + controlFal.message;
                    }
                }
                if (controlFal.status != 200)
                {
                    if (controlFal.Solicitudes.Count != 0)
                    {
                        msj.status = controlFal.status;
                        msj.message = controlFal.message;
                        return View("Error", msj);
                    }
                }
                ViewData["Title"] = "Asignados";
                return View("Asignados", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult Repara(int pagina, string XT, string cveEmp, string Buscar, DateTime FehTick, int TipTicket, int TipFalla, int NumTicket, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    TempData["FehTick"] = FehTick;
                }
                if (Convert.ToInt32(Buscar) == 0)
                {
                    //controlFal = con.CargarCat(cveEmp, 4);
                    controlFal = cone.PrimerCarga(4, cveEmp, "", 0, 0, 0, 0, 0, 0, true);
                    //controlFal = cone.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                    ViewData["Title"] = "Reparacion";
                }
                else if (Convert.ToInt32(Buscar) == 1)
                {
                    //return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick, TipTicket, TipFalla, NumTicket, pagina, pageSize, idsub });
                    return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick, TipTicket, TipFalla, NumTicket, idsub });
                }
                ViewData["Title"] = "Reparacion";
                return View("Reparacion", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet, HttpPost]
        public IActionResult BuscarReparacion(string XT, string cveEmp, DateTime FehTick, int TipTicket, int TipFalla, int NumTicket, int pagina, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;

                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick;
                }
                else
                {
                    TempData["FehTick"] = FehTick;
                }
                if (NumTicket == 0)
                {
                    //controlFal = con.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, pagina, pageSize);
                    controlFal = cone.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, false);
                    //controlFal = cone.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                }
                else
                {
                    //controlFal = con.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, pagina, pageSize);
                    controlFal = cone.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, false);
                    //controlFal = cone.PrimerCarga(4, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                }
                if (controlFal.status == 200)
                {
                    TempData["Buscar"] = 1;
                }
                else if (controlFal.status == 400)
                {
                    TempData["Mensaje"] = "¡" + controlFal.status + " " + controlFal.message + "!";
                    TempData["NumTicket"] = NumTicket;
                    TempData["ClaveTipoFalla"] = TipFalla;
                    TempData["TipTicket"] = TipTicket;
                }
                if (controlFal.status > 400)
                {
                    if (controlFal.Solicitudes.Count != 0)
                    {
                        msj.status = controlFal.status;
                        msj.message = controlFal.message;
                        return View("Error", msj);
                    }
                }
                ViewData["Title"] = "Reparacion";
                return View("Reparacion", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult AsigRepa(string XT, string cveEmp, string NumTicket, DateTime FechEstima, DateTime FechEstimaComparar, bool AttPar, string ComeMotvAsig, int idsub, int pagina, int Diesel, int Grua, int ClaveTipoApoyo, string TipoApoyo, string TipoEquipo)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;

                //if (ClaveTipoApoyo == 2)
                //{
                //    controlFal = con.Primer_ordenes(4, model.Data[0].EmpS[0].cveEmp.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, model.Data[0].idus, 0, idsub, pagina, pageSize);
                //    ViewData["Title"] = "Reparacion";
                //    TempData["ShowModal"] = "True";
                //    TempData["FehTick"] = FechEstima;
                //    TempData["NumTicket"] = NumTicket;
                //    TempData["TipoEquipo"] = TipoEquipo;
                //    return View("Reparacion", controlFal);
                //}
                if (FechEstima > FechEstimaComparar && FechEstima > DateTime.Now)
                {
                    //controlFal = con.ModificadorFall(4, 5, model.Data[0].EmpS[0].cveEmp.ToString(), NumTicket, "0", 0, 0, FechEstima.ToString("yyyy-MM-dd HH:mm:ss"), ComeMotvAsig, model.Data[0].idus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub, pagina, pageSize, Diesel, Grua, "", "", "", 0, AttPar);
                    controlFal = cone.ModificadorFall(4, 5, model.Data[0].EmpS[0].cveEmp.ToString(), NumTicket, "0", 0, 0, FechEstima.ToString("yyyy-MM-dd HH:mm:ss"), ComeMotvAsig, model.Data[0].idus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub, Diesel, Grua, "", "", "", 0, AttPar, null);
                    if (controlFal.status == 200)
                    {
                        TempData["Buscar"] = 0;
                        ViewData["Title"] = "Reparacion";
                        TempData["guardado"] = controlFal.status + "\r\n¡" + controlFal.message + "!";
                        TempData["FehTick"] = FechEstima;
                        return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick = DateTime.Now, TipTicket = 0, TipFalla = 0, NumTicket = 0, pagina, idsub });
                    }
                    else
                    {
                        TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                        ViewData["Title"] = "Reparacion";
                        TempData["FehTick"] = FechEstima;
                        return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick = DateTime.Now, TipTicket = 0, TipFalla = 0, NumTicket = 0, pagina, idsub });
                    }
                }
                if (FechEstima == FechEstimaComparar)
                {
                    //controlFal = con.ModificadorFall(4, 5, model.Data[0].EmpS[0].cveEmp.ToString(), NumTicket, "0", 0, 0, FechEstima.ToString("yyyy-MM-dd HH:mm:ss"), ComeMotvAsig, model.Data[0].idus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub, pagina, pageSize, Diesel, Grua, "", "", "", 0, AttPar);
                    controlFal = cone.ModificadorFall(4, 5, model.Data[0].EmpS[0].cveEmp.ToString(), NumTicket, "0", 0, 0, FechEstima.ToString("yyyy-MM-dd HH:mm:ss"), ComeMotvAsig, model.Data[0].idus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, idsub, Diesel, Grua, "", "", "", 0, AttPar, null);
                    if (controlFal.status == 200)
                    {
                        TempData["Buscar"] = 0;
                        ViewData["Title"] = "Reparacion";
                        TempData["guardado"] = controlFal.status + "\r\n¡" + controlFal.message + "!";
                        TempData["FehTick"] = FechEstima;
                        return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick = DateTime.Now, TipTicket = 0, TipFalla = 0, NumTicket = 0, pagina, idsub });
                    }
                    else
                    {
                        TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                        ViewData["Title"] = "Reparacion";
                        TempData["FehTick"] = FechEstima;
                        return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick = DateTime.Now, TipTicket = 0, TipFalla = 0, NumTicket = 0, pagina, idsub });
                    }
                }
                else
                {
                    TempData["Buscar"] = 0;
                    TempData["FehTick"] = DateTime.Now;
                    TempData["Mensaje"] = "¡Seleccione una fecha mayor que la registrada " + FechEstima.ToString("yyyy-MM-dd HH:mm:ss") + " !";
                    return RedirectToAction("BuscarReparacion", new { XT, cveEmp, FehTick = DateTime.Now, TipTicket = 0, TipFalla = 0, NumTicket = 0, pagina, idsub });
                }
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult Fin(int pagina, string XT, string cveEmp, string Buscar, DateTime FehTick, int TipTicket, int TipFalla, int NumTicket, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;

                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick;
                }
                else
                {
                    TempData["FehTick"] = FehTick;
                }
                //controlFal = con.CargarCat(cveEmp, 5);
                controlFal = cone.PrimerCarga(5, cveEmp, "", 0, 0, 0, 0, 0, 0, true);
                //controlFal = cone.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                if (controlFal.status == 200)
                {
                    TempData["Buscar"] = 1;
                    TempData["guardado"] = controlFal.status + " ¡" + controlFal.message + "\r\n! \r\n ";
                }
                else
                {
                    TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                }
                ViewData["Title"] = "Finalizado";
                return View("Finalizado", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet, HttpPost]
        public IActionResult BuscarFinalizados(string XT, string cveEmp, DateTime FehTick, int TipTicket, int TipFalla, int NumTicket, int pagina, int idsub)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (FehTick == null || FehTick == DateTime.MinValue)
                {
                    FehTick = DateTime.Now;
                    TempData["FehTick"] = FehTick;
                }
                else
                {
                    TempData["FehTick"] = FehTick;
                }
                if (NumTicket == 0)
                {
                    //controlFal = cone.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                    controlFal = cone.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, false);
                    //controlFal = con.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), FehTick.ToString("yyyy-MM-dd HH:mm:ss"), NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, pagina, pageSize);
                }
                else
                {
                    //controlFal = cone.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub);
                    controlFal = cone.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, false);
                    //controlFal = con.PrimerCarga(5, model.Data[0].EmpS[0].cveEmp.ToString(), null, NumTicket, TipTicket, TipFalla, model.Data[0].idus, 0, idsub, pagina, pag eSize);
                }
                if (controlFal.status == 200)
                {
                    TempData["Buscar"] = 1;
                }
                else
                {
                    TempData["Mensaje"] = controlFal.status + " ¡" + controlFal.message + "\r\n Intenta mas Tarde! \r\n ";
                }
                if (controlFal.status > 400)
                {
                    msj.status = controlFal.status;
                    msj.message = controlFal.message;
                    return View("Error", msj);
                }
                ViewData["Title"] = "Finalizado";
                TempData["FehTick"] = FehTick.ToString("yyyy-MM-dd HH:mm:ss");
                ViewData["TipTicket"] = TipTicket;
                ViewData["TipFalla"] = TipFalla;

                return View("Finalizado", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public IActionResult Consul(string XT, string cveEmp, int NumTicket, DateTime FehInicio, DateTime FehFin, int idsub, int pagina)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;

                if (NumTicket == 0)
                {
                    NumTicket = 0;
                }
                if (FehInicio == null || FehInicio == DateTime.MinValue)
                {
                    FehInicio = DateTime.MinValue;
                }
                if (FehFin == null || FehFin == DateTime.MinValue)
                {
                    FehFin = DateTime.Now;
                }
                //controlFal = con.ConsultaGeneral(cveEmp, NumTicket, FehFin.ToString("yyyy-MM-dd HH:mm:ss"), FehInicio.ToString("yyyy-MM-dd HH:mm:ss"), pagina, pageSize);
                controlFal = cone.ConsultaGeneral(cveEmp, NumTicket, FehFin.ToString("yyyy-MM-dd HH:mm:ss"), FehInicio.ToString("yyyy-MM-dd HH:mm:ss"));
                if (controlFal.Solicitudes.Count == 0)
                {
                    if (controlFal.TotalSolicitudes == null) 
                    {
                        controlFal.TotalSolicitudes = 0; 
                    }
                }
                else
                {
                    for (int i = 0; i < controlFal.Solicitudes.Count; i++)
                    {
                        controlFal.Solicitudes[i].PathArchivo = ObtenerArchivosPorTicket(controlFal.Solicitudes[i].NumTicket);
                    }
                }
                ViewData["UsuarioModel"] = model;
                TempData["FehInicio"] = FehInicio;
                TempData["FehFin"] = FehFin;
                ViewData["Title"] = "Consulta";
                return View("Consulta", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet, HttpPost]
        public IActionResult BusConsul(string XT, DateTime FehInicio, DateTime FehFin, int NumTicket, string cveEmp, int idsub, int pagina)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                    return RedirectToAction("Index", "Loging");

                model = menu.RegresMenu(UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]), UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]), Convert.ToInt32(cveEmp), url, XT);
                model.Token = XT;
                model.idsub = idsub;
                ViewData["UsuarioModel"] = model;
                if (NumTicket == 0)
                {
                    if (FehInicio == null || FehInicio == DateTime.MinValue)
                    {
                        TempData["Mensaje"] = "¡Seleccione una Fecha de Inicio!";
                    }
                    else
                    {
                        //controlFal = con.ConsultaGeneral(cveEmp, NumTicket, FehInicio.ToString("yyyy-MM-dd HH:mm:ss"), FehFin.ToString("yyyy-MM-dd HH:mm:ss"), pagina, pageSize);
                        controlFal = cone.ConsultaGeneral(cveEmp, NumTicket, FehInicio.ToString("yyyy-MM-dd HH:mm:ss"), FehFin.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                else
                {
                    //controlFal = con.ConsultaGeneral(cveEmp, NumTicket, null, null, pagina, pageSize);
                    controlFal = cone.ConsultaGeneral(cveEmp, NumTicket, null, null);
                }
                if (controlFal.status == 200)
                {
                    if (controlFal.TotalSolicitudes == null) { controlFal.TotalSolicitudes = 0; }
                    for (int i = 0; i < controlFal.Solicitudes.Count; i++)
                    {
                        controlFal.Solicitudes[i].PathArchivo = ObtenerArchivosPorTicket(controlFal.Solicitudes[i].NumTicket);
                    }
                    TempData["FehInicio"] = FehInicio;
                    TempData["FehFin"] = FehFin;
                }
                if (controlFal.status == 400)
                {
                    if (controlFal.TotalSolicitudes == null)
                    {
                        controlFal.TotalSolicitudes = 0;
                    }
                    TempData["Mensaje"] = controlFal.status + "\r\n ¡" + controlFal.message + "\r\n!";
                    TempData["FehFin"] = FehFin;
                }
                ViewData["Title"] = "Consulta";
                return View("Consulta", controlFal);
            }
            catch (Exception e)
            {
                msj.status = 400;
                msj.message = "Error de Conexion | Erorr Desconocido Notificar a Sistemas Desarrollo" + e.Message.ToString();
                return View("Error", msj);
            }
        }
        private List<archivo> ObtenerArchivosPorTicket(int numTicket)
        {
            List<archivo> listarch = new List<archivo>();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\GMI\\" + numTicket.ToString());

            if (Directory.Exists(path))
            {
                string[] fileNames = Directory.GetFiles(path);

                foreach (var fileName in fileNames)
                {
                    listarch.Add(new archivo { carpet = numTicket, exte = Path.GetExtension(fileName).ToString(), rutFile = numTicket.ToString() + "/" + Path.GetFileName(fileName) });
                }
            }
            return listarch;
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost, HttpGet]
        public async Task<ActionResult> SubirArchivo(List<IFormFile> Files, int pagina, string XT, string cveEmp, DateTime FehInicio, DateTime FehFin, int NumTicket, int idsub)
        {
            try
            {
                foreach (var archivo in Files)
                {
                    if (archivo != null && archivo.Length > 0)
                    {
                        var extension = Path.GetExtension(archivo.FileName).ToLower();

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\GMI\\" + NumTicket.ToString());

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        string fileName = NumTicket.ToString() + "_" + Guid.NewGuid().ToString() + extension;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            using (var image = Image.Load(archivo.OpenReadStream()))
                            {
                                image.Mutate(x => x.Resize(800, 600));
                                image.Save(fileNameWithPath);
                            }
                        }
                        else if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                        {
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }
                        }
                    }
                }
                TempData["guardado"] = "Se subieron correctamente las imagenes";
                return RedirectToAction("BusConsul", new { XT, FehInicio, FehFin, NumTicket = 0, cveEmp, idsub, pagina });
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
