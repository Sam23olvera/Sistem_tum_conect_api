using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net;

namespace ConectDB.DB
{
    public class ConectRegistraFalla
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        DataApi hh = new DataApi();
        RootData dataenvio = new RootData();
        JObject js = new JObject();
        JObject JRespuesta = new JObject();
        ModelFallas zcioFallas = new ModelFallas();
        JArray? data = null;

        public ModelFallas ObjetoModelOperadores_Rem(string empresa)
        {
            try
            {
                dataenvio.data.bdCc = 5;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPQRY2_CatIniMantenimiento";
                dataenvio.filter.Clear();
                dataenvio.filter.Add(new Elements { property = "ClaveEmpresa", value = empresa });
                js = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
                JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, js));
                data = JRespuesta["data"] as JArray;
                if (data != null && data.Count > 0)
                {
                    zcioFallas = JsonConvert.DeserializeObject<ModelFallas>(data[3].ToString());
                    zcioFallas.TBCAT_Unidades = JsonConvert.DeserializeObject<ModelFallas>(data[0].ToString()).TBCAT_Unidades;
                    zcioFallas.TBCAT_Remolques = JsonConvert.DeserializeObject<ModelFallas>(data[1].ToString()).TBCAT_Remolques;
                    zcioFallas.TBCAT_Operador = JsonConvert.DeserializeObject<ModelFallas>(data[2].ToString()).TBCAT_Operador;
                    zcioFallas.TBCAT_TipoOp = JsonConvert.DeserializeObject<ModelFallas>(data[4].ToString()).TBCAT_TipoOp;
                }
                return zcioFallas;
            }
            catch (Exception e)
            {
                zcioFallas.Eror[0].status = 400;
                zcioFallas.Eror[0].message = e.Message;
                return zcioFallas;
            }
        }

        public ModelFallas Guardar(ModelFallas fallas, int idus)
        {
            try
            {
                TicketFalla ticket = new TicketFalla
                {
                    Ticket = new Ticket
                    {
                        CveEmpresa = fallas.cveEmp,
                        CveOrigTicket = fallas.selorigen,
                        CveAccion = fallas.selAccion,
                        CveUnidad = fallas.seleuni,
                        CveOperador = fallas.Opera,
                        CveRemolque1 = fallas.remolque1,
                        CveRemolque2 = fallas.remolque2,
                        CveDolly = fallas.selDolly,
                        CveTipoOperacion = fallas.SelectOperacion,
                        CveTipoCarga = fallas.selTipCarga,
                        TelOperador = fallas.telopera,
                        CveEstatus = 1, // Estatus inicial
                        FechaUltPosGps = fallas.fechaGPs,
                        DirPosGps = fallas.DirPosGps == "undefined" ? "" : fallas.DirPosGps,
                        LatGPS = fallas.latitud,
                        LonGPS = fallas.longitud,
                        LatNva = fallas.latitudNew,
                        LonNva = fallas.longitudNew,
                        ConViaje = fallas.inCheckViaje,
                        CveViajeTUM = fallas.ClvViajTum,
                        Fallas = new List<Falla>()
                    }
                };
                if (!string.IsNullOrEmpty(fallas.clavesFalAndComen))
                {
                    var fallasClaves = fallas.clavesFalAndComen.Split('%', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < fallasClaves.Length; i++)
                    {
                        var claveData = fallasClaves[i].Split('|', StringSplitOptions.RemoveEmptyEntries);

                        if (claveData.Length >= 4) // Mínimo para procesar CveOrigenFalla, CveEquipo, CveTipoClasifn y CveTipoFalla
                        {
                            Falla nuevaFalla = new Falla
                            {
                                CveOrigenFalla = int.TryParse(claveData[0], out int origenFalla) ? origenFalla : 0,
                                CveEquipo = int.TryParse(claveData[1], out int CveEquipo) ? CveEquipo : 0,
                                CveTipoClasifn = int.TryParse(claveData[2], out int clasificacion) ? clasificacion : 0,
                                CveTipoFalla = int.TryParse(claveData[3], out int tipoFalla) ? tipoFalla : 0,
                                DescripFalla = claveData[4]
                            };
                            if (int.Parse(claveData[2]) == 2)
                            {
                                string[] llantas = fallas.fallallantas.Split('%', StringSplitOptions.RemoveEmptyEntries);
                                //string[] llantas = fallas.fallallantas != null ? fallas.fallallantas.Split('%', StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();
                                // LlantaData será nula si no    hay datos de llantas
                                for (int j = 0; j < llantas.Length; j++)
                                {
                                    string[] llantaData = llantas[j].Split('|', StringSplitOptions.RemoveEmptyEntries);
                                    Falla x1 = new Falla();
                                    x1.CveOrigenFalla = nuevaFalla.CveOrigenFalla;
                                    x1.CveEquipo = nuevaFalla.CveEquipo;
                                    x1.CveTipoClasifn = nuevaFalla.CveTipoClasifn;
                                    x1.CveTipoFalla = nuevaFalla.CveTipoFalla;
                                    x1.DescripFalla = nuevaFalla.DescripFalla;

                                    if (llantaData != null && llantaData.Length >= 5) // Mínimo para procesar DOT, Marca, MEDIDA, POSICION, ECOLlanta
                                    {
                                        x1.DOT = llantaData[1];
                                        x1.Marca = llantaData[2];
                                        x1.MEDIDA = llantaData[3];
                                        x1.POSICION = int.TryParse(llantaData[4], out int posicion) ? posicion : 0;
                                        x1.ECOLlanta = llantaData[5];
                                    }
                                    ticket.Ticket.Fallas.Add(x1);
                                }
                            }
                            else 
                            {
                                ticket.Ticket.Fallas.Add(nuevaFalla);
                            }
                        }
                    }
                }

                string jsonString = JsonConvert.SerializeObject(ticket, Formatting.Indented);
                JObject JsonTicket = JObject.Parse(jsonString);


                dataenvio.data.bdCc = 5;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPINS_ControlTicket";
                dataenvio.filter.Clear();
                dataenvio.filter.Add(new Elements { property = "Dat1", value = JsonTicket.ToString() });
                dataenvio.filter.Add(new Elements { property = "cveUsuario", value = idus.ToString() });
                js = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
                JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, js));
                if (JRespuesta["status"].ToString() == "200")
                {
                    zcioFallas.Eror = new List<Error> { new Error { status = Convert.ToInt16(JRespuesta["status"].ToString()), message = JRespuesta["message"].ToString() } };
                }
                else
                {
                    zcioFallas.Eror = new List<Error> { new Error { status = Convert.ToInt16(JRespuesta["status"].ToString()), message = JRespuesta["message"].ToString() } };
                }
                return zcioFallas;
            }
            catch (Exception e)
            {
                zcioFallas.Eror[0].status = 400;
                zcioFallas.Eror[0].message = e.Message;
                return zcioFallas;
            }
        }
    }
}
