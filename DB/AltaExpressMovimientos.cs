﻿using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static ConectDB.Models.LogUser;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace ConectDB.DB
{
    public class AltaExpressMovimientos
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        private readonly DataApi hh = new DataApi();
        //RootData dataenvio = new RootData();
        RootData dataenvio = new RootData { data = new Data(), filter = new List<Elements>() };
        JObject JS = new JObject();
        JObject JRespuesta = new JObject();
        JArray data = new JArray();
        AltaExpressModel REpuestaModel = new AltaExpressModel();
        public AltaExpressModel Catalogos(int ClaveEmpresa)
        {
            try
            {
                dataenvio.data.bdCc = 2;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPQRY_CatInicialAltaExp";
                dataenvio.filter.Clear();
                dataenvio.filter.Add(new Elements { property = "ClaveEmpresa", value = ClaveEmpresa.ToString() });
                JS = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
                JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, JS));
                data = JRespuesta["data"] as JArray;
                if (data != null && data.Count > 0)
                {
                    REpuestaModel = JsonConvert.DeserializeObject<AltaExpressModel>(data[0].ToString());
                }
                return REpuestaModel;
            }
            catch (Exception ex)
            {
                REpuestaModel.Errors[0].status = 400;
                REpuestaModel.Errors[0].message = ex.Message;
                return REpuestaModel;
            }
        }
        public AltaExpressModel Guardar(AltaExpressModel guar)
        {
            try
            {
                if (guar.Errors == null)
                    guar.Errors = new List<Error>();

                List<TiposUni> Tipo = new List<TiposUni>();
                string[] idsuni = new string[7];
                string status = "";
                string message = "";
                string Mensaje = "";
                if (guar.RangoMedio)
                    idsuni[0] = "1";

                if (guar.Thorton)
                    idsuni[1] = "2";
                if (guar.Rabon)
                    idsuni[2] = "3";
                if (guar.Camioneta)
                    idsuni[3] = "4";
                if (guar.TractoSenci)
                    idsuni[4] = "5";
                if (guar.VHlig)
                    idsuni[5] = "6";
                if (guar.TracFull)
                    idsuni[6] = "7";
                foreach (var id in idsuni)
                {
                    if (!string.IsNullOrEmpty(id))
                        Tipo.Add(new TiposUni { IdTipoUnidadOp = Convert.ToInt16(id) });
                }
                SPINSCandidatoAE envio = new SPINSCandidatoAE
                {
                    IdTipoTrab = guar.CveTipoEmp,
                    Apaterno = guar.ApPaterno,
                    AMaterno = guar.ApMaterno,
                    Nombres = guar.Nombre,
                    IdPais = guar.SelPais,//nacionalidad
                    IdEstado = guar.selEstado,
                    IdMpio = guar.seleMuni,
                    IdColonia = guar.Colonia,
                    Calle = guar.Calle,
                    NoExt = guar.NumExterior,
                    NoInt = guar.NumInterior,
                    CP = guar.CP,
                    ClaveOrigi = guar.originario,
                    TCel = guar.Cel,
                    TLocal = guar.Tel,
                    FechaNac = guar.FechNac?.ToString("yyyy-MM-dd"),
                    CURP = guar.CURP,
                    RFC = guar.RFC,
                    NSS = guar.NSS,
                    Genero = guar.sexo,
                    IdEscolar = guar.Escol,
                    Concluida = guar.EscoConcluida,
                    Reingreso = guar.Reingreso,
                    IdTipoLicen = guar.SeleLic,
                    NumLicen = guar.NumLicen,
                    VigenciaLicen = guar.VigenciaLicen?.ToString("yyyy-MM-dd"),
                    RControl = guar.RControl,
                    ConInfonavit = guar.ConInfonavit,
                    FolInfonavit = guar.FolInfonavit,
                    Experiencia = guar.Experiencia,
                    ConFonacot = guar.ConFonacot,
                    FolFonacot = guar.FolFonacot,
                    IdTipoOp = guar.TipOpera,
                    IdZonaTrab = guar.ZonTra,
                    IdBanco = guar.Banc,
                    CuentaBanca = guar.CuentaBanca,
                    IdPuesto = guar.selePues,
                    ClaveSalario = guar.selSal,
                    AnoExperiencia = guar.AnoExperiencia,
                    VigenciaMedic = guar.AptMedi?.ToString("yyyy-MM-dd"),
                    EdoCivil = guar.EdoCivil,
                    TiposUnis = Tipo
                };

                string jsonString = JsonConvert.SerializeObject(envio, Formatting.Indented);


                dataenvio.data.bdCc = 2;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPINS_CandidatoAE";
                dataenvio.filter.Clear();
                dataenvio.filter.Add(new Elements { property = "Data", value = jsonString });

                JS = JObject.Parse(JsonConvert.SerializeObject(dataenvio));

                JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, JS));
                status = JRespuesta["status"]?.ToString();
                message = JRespuesta["message"]?.ToString();
                Mensaje = $"{status} {message}";

                guar.Errors.Add(new Error
                {
                    status = (status == "200") ? 200 : 400,
                    message = Mensaje
                });
                return guar;
            }
            catch (Exception ex)
            {
                guar.Errors.Add(new Error
                {
                    status = 400,
                    message = ex.Message.ToString()
                });
                return guar;
            }
        }
        public AltaExpressModel Impresion(string alta)
        {
            try
            {

                dataenvio.data.bdCc = 2;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPQRY_Cat_Imprime";
                dataenvio.filter.Clear();
                dataenvio.filter.Add(new Elements { property = "clavCan", value = alta });
                JS = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
                JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, JS));
                // Obtener el array "data"
                data = JRespuesta["data"] as JArray;
                // Verificamos si "data" contiene elementos
                if (data != null && data.Count > 0)
                {
                    JObject firstData = (JObject)data[0]; // Convertimos el primer elemento en JObject
                    JArray tbcAt_AltaExp = firstData["TBCAT_AltaExp"] as JArray;
                    JArray tbcAt_UnidaExperiencia = firstData["TBCAT_UnidaExperiencia"] as JArray;
                    JArray tbcAt_DoctoRecibidos = firstData["TBCAT_DoctoRecibidos"] as JArray;

                    if (tbcAt_AltaExp != null && tbcAt_AltaExp.Count > 0)
                    {
                        REpuestaModel = JsonConvert.DeserializeObject<AltaExpressModel>(tbcAt_AltaExp[0].ToString());
                    }
                    if (tbcAt_UnidaExperiencia != null && tbcAt_UnidaExperiencia.Count > 0)
                    {
                        REpuestaModel.tbcAt_UnidaExperiencia = JsonConvert.DeserializeObject<List<TipUn>>(tbcAt_UnidaExperiencia.ToString());
                    }
                    if (tbcAt_DoctoRecibidos != null && tbcAt_DoctoRecibidos.Count > 0) 
                    {
                        REpuestaModel.TBCAT_DoctoRecibidos = JsonConvert.DeserializeObject<List<TBCATDoctoRecibido>>(tbcAt_DoctoRecibidos.ToString());
                    }
                    REpuestaModel.Errors = new List<Error> { new Error { status = 200, message = JRespuesta["status"]?.ToString() + JRespuesta["message"]?.ToString()+ "Listo para agregar Archivos del Alta |" + alta } };
                }
                return REpuestaModel;
            }
            catch (Exception ex)
            {
                REpuestaModel.Errors.Add(new Error
                {
                    status = 400,
                    message = ex.Message.ToString()
                });
                return REpuestaModel;

            }
        }
    }
}
