﻿using ConectDB.DB;
using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ConectDB.Models.LogUser;

namespace ConectDB.Controllers
{
    public class RegistroFallasController : Controller
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/accesyst";
        ConectRegistraFalla con = new ConectRegistraFalla();
        ConectMenuUser menu = new ConectMenuUser();
        UsuarioModel model = new UsuarioModel();
        ModelFallas oLista = new ModelFallas();
        Email envio = new Email();

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int cveEmp, string XT)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                return RedirectToAction("Index", "Loging");

            string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
            string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

            model = menu.RegresMenu(desusuario, descontraseña, cveEmp, url, XT);
            ViewData["UsuarioModel"] = model;
            model.Token = XT;
            oLista = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());
            return View("Index", oLista);

        }
        [HttpPost]
        public IActionResult Guardar(int ClaveTipoTicket, int TipoClas, TBCATTipoFalla tipoFalla, string Dot, string Marca, string Medida, int Posis, string ComeFalla, TBCATOperador operador, string telop, TBCATUnidade unidade, TBCATRutum rutum, int opcionesRemolque1, TBCATTipoCarga carga, int cvTipoequipo, string UbiRepor, string TramCarretero, TBCATTipoApoyo tBCATTipo, string LongGps, string LatGps, string DirGPS, string FechGPS, string Token, string Emp, int CheckDisel, int CheckGrua, int ClaOpDol,int selDolly)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["usuario"]) || string.IsNullOrEmpty(HttpContext.Request.Cookies["contra"]))
                return RedirectToAction("Index", "Loging");

            string desusuario = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["usuario"]);
            string descontraseña = UrlEncryptor.DecryptUrl(HttpContext.Request.Cookies["contra"]);

            try
            {
                model = menu.RegresMenu(desusuario, descontraseña, Convert.ToInt32(Emp), url, Token);
                ViewData["UsuarioModel"] = model;
                ViewData["Token"] = Token;
                oLista = con.ObjetoModelOperadores_Rem(model.Data[0].EmpS[0].cveEmp.ToString());

                string jsonEnvio = "{'RegistFalla':[";
                jsonEnvio += "{";
                if (ClaveTipoTicket == 0)
                {
                    TempData["Mensaje"] = "¡Seleccione el Tipo de Ticket!";
                    return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'ClavTipTick':" + ClaveTipoTicket.ToString() + ",";
                }
                if (TipoClas == 0)
                {
                    jsonEnvio += "'ClvTipClasif': 0,";
                    TempData["Mensaje"] = "¡Seleccione una Clasificación!";
                    return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'ClvTipClasif': " + TipoClas.ToString() + ",";
                    if (string.IsNullOrEmpty(Dot))
                    {
                        jsonEnvio += "'DOT': '',";
                    }
                    else
                    {
                        jsonEnvio += "'DOT': '" + Dot + "',";
                    }
                    if (string.IsNullOrEmpty(Marca))
                    {
                        jsonEnvio += "'Marca': '',";
                    }
                    else
                    {
                        jsonEnvio += "'Marca': '" + Marca + "',";
                    }
                    if (string.IsNullOrEmpty(Medida))
                    {
                        jsonEnvio += "'Medida': '',";
                    }
                    else
                    {
                        jsonEnvio += "'Medida': '" + Medida + "',";
                    }
                    if (TipoClas == 2)
                    {
                        if (Posis == 0)
                        {
                            jsonEnvio += "'Posicion': 0,";
                            TempData["Mensaje"] = "¡Debes de Colocar Por lo Menos la Posicion!";
                            return View("Index", oLista);
                        }
                        else
                        {
                            jsonEnvio += "'Posicion': " + Posis.ToString() + ",";
                        }
                    }
                    else
                    {
                        jsonEnvio += "'Posicion': " + Posis.ToString() + ",";
                    }
                    if (string.IsNullOrEmpty(ComeFalla))
                    {
                        jsonEnvio += "'ComFalla': '',";
                    }
                    else
                    {
                        jsonEnvio += "'ComFalla': '" + ComeFalla + "',";
                    }
                }
                if (tipoFalla.ClaveTipoFalla == 0)
                {
                    jsonEnvio += "'ClvFalla':'0',";
                    TempData["Mensaje"] = "¡Seleccione un Tipo Falla!";
                    return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'ClvFalla': '" + tipoFalla.ClaveTipoFalla + "',";
                }

                if (operador.ClaveOperador == 0)
                {
                    jsonEnvio += "'ClavOperador': 0 ,";
                    TempData["Mensaje"] = "¡Seleccione un operador!";
                    return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'ClavOperador':" + operador.ClaveOperador + ",";
                }
                if (string.IsNullOrEmpty(telop))
                {
                    jsonEnvio += "'TelOp': '',";
                }
                else
                {
                    jsonEnvio += "'TelOp':'" + telop + "',";
                }
                if (unidade.ClaveUnidad_Motora == 0)
                {
                    if (ClaveTipoTicket == 1)
                    {
                        TempData["Mensaje"] = "¡Seleccione un Economico!";
                        return View("Index", oLista);
                    }
                    jsonEnvio += "'ClvUniMot': 0,";
                }
                else
                {
                    jsonEnvio += "'ClvUniMot':" + unidade.ClaveUnidad_Motora + ",";
                }
                if (unidade.Numero == 0)
                {
                    jsonEnvio += "'Unidad':0 ,";
                }
                else
                {
                    jsonEnvio += "'Unidad':" + unidade.Numero + ",";
                }
                if (string.IsNullOrEmpty(unidade.Alias))
                {
                    jsonEnvio += "'Alias': '' ,";
                    //TempData["Mensaje"] = "¡Seleccione un Alias!";
                    //return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'Alias':'" + unidade.Alias + "',";
                }
                if (rutum.CveRuta == 0)
                {
                    jsonEnvio += "'ClvRut': 0 ,";
                }
                else
                {
                    jsonEnvio += "'ClvRut':" + rutum.CveRuta + ",";
                }
                if (opcionesRemolque1 == 0)
                {
                    jsonEnvio += "'Remolque1': '" + opcionesRemolque1 + "',";
                    //TempData["Mensaje"] = "¡Seleccione un Remolque!";
                    //return View("Index", oLista);
                }
                else
                {
                    jsonEnvio += "'Remolque1': '" + opcionesRemolque1 + "',";
                }
                if (carga.ClaveTipoCarga == 0)
                {
                    jsonEnvio += "'ClvTCarg': 0 ,";
                }
                else
                {
                    jsonEnvio += "'ClvTCarg':" + carga.ClaveTipoCarga + ",";
                }
                if (cvTipoequipo == 0)
                {
                    jsonEnvio += "'ClvTipEq': 0 ,";
                }
                else
                {
                    jsonEnvio += "'ClvTipEq':" + cvTipoequipo + ",";
                }
                jsonEnvio += "'ClavEst': 1, ";
                if (string.IsNullOrEmpty(UbiRepor))
                {
                    jsonEnvio += "'UbiRepor': '',";
                }
                else
                {
                    jsonEnvio += "'UbiRepor': '" + UbiRepor + "',";
                }
                if (string.IsNullOrEmpty(TramCarretero))
                {
                    jsonEnvio += "'TramCar': '',";
                }
                else
                {
                    jsonEnvio += "'TramCar': '" + TramCarretero + "',";
                }
                if (tBCATTipo.ClaveTipoApoyo == 0)
                {
                    jsonEnvio += "'ClvApoyo': 0 ,";
                }
                else
                {
                    jsonEnvio += "'ClvApoyo': " + tBCATTipo.ClaveTipoApoyo + ",";
                }
                if (string.IsNullOrEmpty(LongGps))
                {
                    jsonEnvio += "'LongGPS': 0,";
                }
                else
                {
                    jsonEnvio += "'LongGPS': " + LongGps + ",";
                }
                if (string.IsNullOrEmpty(LatGps))
                {
                    jsonEnvio += "'LatGps': 0 ,";
                }
                else
                {
                    jsonEnvio += "'LatGps': " + LatGps + ",";
                }
                if (string.IsNullOrEmpty(DirGPS))
                {
                    jsonEnvio += "'DirGPS': null,";
                }
                else
                {
                    jsonEnvio += "'DirGPS': '" + DirGPS + "',";
                }
                if (string.IsNullOrEmpty(FechGPS))
                {
                    jsonEnvio += "'FechGPS': null,";
                }
                if (FechGPS == "1900-01-01 00:00:00.000")
                {
                    jsonEnvio += "'FechGPS': null,";
                }
                if (FechGPS == "1900-01-01")
                {
                    jsonEnvio += "'FechGPS': null,";
                }
                if (FechGPS == "1900-01-01T00:00:00")
                {
                    jsonEnvio += "'FechGPS': null,";
                }
                else
                {
                    jsonEnvio += "'FechGPS': '" + FechGPS + "',";
                }
                jsonEnvio += "'CvInUser':'" + model.Data[0].idus + "',";
                jsonEnvio += "'ClvEmpresa':" + Emp + ",";
                jsonEnvio += "'Diesel':" + CheckDisel + ",";
                jsonEnvio += "'Grua':" + CheckGrua + ",";
                if (ClaOpDol == 0) 
                {
                    jsonEnvio += "'CveTipoOpe': 0,";
                }
                else
                {
                    jsonEnvio += "'CveTipoOpe':" + ClaOpDol + ",";
                }
                if (selDolly == 0) 
                {
                    jsonEnvio += "'CveDolly': 0";
                } 
                else 
                {
                    jsonEnvio += "'CveDolly':" + selDolly + ""; 
                }
                jsonEnvio+=  "},";
                jsonEnvio += "]}";
                jsonEnvio = jsonEnvio.Replace(",]}", "]}").Replace("\\", "");

                JObject js = con.GuardarFallas(jsonEnvio);
                if (js["status"]?.ToString() == "400")
                {
                    TempData["Mensaje"] = js["message"]?.ToString();
                    return View("Index", oLista);
                }
                if (js["status"]?.ToString() == "200")
                {
                    if (js["data"] == null)
                    {
                        TempData["guardado"] = js["message"]?.ToString() + " true \n" + "\nNo se pudo Avisar a Mantenimiento por Correo";
                    }
                    else 
                    {
                        if (envio.EnvioCorreo(model.Data[0].nom, model.Data[0].nomrol, js))
                        {
                            TempData["guardado"] = js["message"]?.ToString() + " true";
                        }
                        else
                        {
                            TempData["guardado"] = js["message"]?.ToString() + " false";
                        }
                    }
                    return View("Index", oLista);
                }
                if (js["status"]?.ToString() == "Desconosido")
                {
                    TempData["Mensaje"] = js["message"]?.ToString();
                    return View("Index", oLista);
                }
                else
                {
                    TempData["Mensaje"] = js.ToString();
                    return View("Index", oLista);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

    }
}
