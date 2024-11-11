using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static ConectDB.Models.LogUser;
using static OfficeOpenXml.ExcelErrorValue;

namespace ConectDB.DB
{
    public class ConexionApiControlReparaciones
    {
        private readonly string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        private readonly DataApi hh = new DataApi();
        RootData dataenvio = new RootData();
        RootData DatEnvio = new RootData();
        ControlFalla? controlFalla = new ControlFalla();
        JObject jconvert = new JObject();
        JObject JRespuesta = new JObject();
        JArray? data = new JArray();
        JArray? dat = new JArray();
        public ControlFalla PrimerCarga_sin_catlog(int CveEstatus, string empresa, string CveUser, string fecha, int UserFiltro, int IdSubmodulo)
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_ControlReparaciones_TEST";
            dataenvio.filter.Add(new Elements { property = "cveEmpresa", value = empresa });
            dataenvio.filter.Add(new Elements { property = "CveEstatus", value = CveEstatus.ToString() });
            dataenvio.filter.Add(new Elements { property = "Fecha", value = fecha });
            dataenvio.filter.Add(new Elements { property = "NumTicket", value = "0" });
            dataenvio.filter.Add(new Elements { property = "TipoTicket", value = "0" });
            dataenvio.filter.Add(new Elements { property = "TipoFalla", value = "0" });
            dataenvio.filter.Add(new Elements { property = "CveUser", value = CveUser });
            dataenvio.filter.Add(new Elements { property = "UserFiltro", value = UserFiltro.ToString() });
            dataenvio.filter.Add(new Elements { property = "IdSubmodulo", value = IdSubmodulo.ToString() });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            data = JRespuesta["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                controlFalla = JsonConvert.DeserializeObject<ControlFalla>(data[0].ToString());
            }
            return controlFalla;
        }
        public ControlFalla PrimerCarga(int CveEstatus, string empresa, string? FehTick, int NumTicket, int TipoTicket, int TipoFalla, int CveUser, int UserFiltro, int IdSubmodulo,bool cat)
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_CatalogosMantto";
            dataenvio.filter.Add(new Elements { property = "ClaveEmpresa", value = empresa });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            data = JRespuesta["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                controlFalla = JsonConvert.DeserializeObject<ControlFalla>(data[0].ToString());
                if (cat == true)
                {
                    controlFalla.Solicitudes = new List<Solicitude>();
                    controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                    controlFalla.message = JRespuesta["message"].ToString();
                }
                else if (cat == false)
                {
                    DatEnvio.data.bdCc = 5;
                    DatEnvio.data.bdSch = "dbo";
                    DatEnvio.data.bdSp = "SPQRY_ControlReparaciones_TEST";
                    DatEnvio.filter.Add(new Elements { property = "cveEmpresa", value = empresa });
                    DatEnvio.filter.Add(new Elements { property = "CveEstatus", value = CveEstatus.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "Fecha", value = FehTick });
                    DatEnvio.filter.Add(new Elements { property = "NumTicket", value = NumTicket.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "TipoTicket", value = TipoTicket.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "TipoFalla", value = TipoFalla.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "CveUser", value = CveUser.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "UserFiltro", value = UserFiltro.ToString() });
                    DatEnvio.filter.Add(new Elements { property = "IdSubmodulo", value = IdSubmodulo.ToString() });
                    jconvert = JObject.Parse(JsonConvert.SerializeObject(DatEnvio));
                    JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
                    dat = JRespuesta["data"] as JArray;
                    if (dat != null && dat.Count > 0)
                    {
                        controlFalla.Solicitudes = JsonConvert.DeserializeObject<ControlFalla>(dat[0].ToString()).Solicitudes;
                        controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                    }
                    else
                    {
                        controlFalla.Solicitudes = new List<Solicitude>();
                        controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                        controlFalla.message = JRespuesta["message"].ToString();
                    }
                }
            }
            return controlFalla;
        }

        public ControlFalla ModificadorFall(int CveEstatus, int modCveEstatus, string empresa, string NumTicket, string UseAsigna, int TipoApoyo, int TipoFalla, string FechaHoraEstimadaReparacion, string ComentariosCambioVto, int CveUser, string Fecha, int Tipotikcet, int idsub, int Diesel, int Grua, string Dot, string Marca, string Medida, int Posis, bool AttPar, string TiemEta)
        {
            if (string.IsNullOrEmpty(FechaHoraEstimadaReparacion))
            {
                FechaHoraEstimadaReparacion = null;
            }
            if (string.IsNullOrEmpty(TiemEta))
            {
                TiemEta = null;
            }
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPMTP_SeguimientoReparaciones_TEST";
            dataenvio.filter.Add(new Elements { property = "claveControlReparaciones", value = NumTicket.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveEstatusNvo", value = modCveEstatus.ToString() });
            dataenvio.filter.Add(new Elements { property = "CveUsuarioAsignado", value = UseAsigna.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveTipoApoyo", value = TipoApoyo.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveTipoClasificacion", value = TipoFalla.ToString() });
            dataenvio.filter.Add(new Elements { property = "FechaHoraEstimadaReparacion", value = FechaHoraEstimadaReparacion });
            dataenvio.filter.Add(new Elements { property = "ComentariosCambioVto", value = ComentariosCambioVto });
            dataenvio.filter.Add(new Elements { property = "CveUsuarioMod", value = CveUser.ToString() });
            dataenvio.filter.Add(new Elements { property = "Diesel", value = Diesel.ToString() });
            dataenvio.filter.Add(new Elements { property = "Grua", value = Grua.ToString() });
            dataenvio.filter.Add(new Elements { property = "DOT", value = Dot.ToString() });
            dataenvio.filter.Add(new Elements { property = "MARCA", value = Marca.ToString() });
            dataenvio.filter.Add(new Elements { property = "MEDIDA", value = Medida.ToString() });
            dataenvio.filter.Add(new Elements { property = "POSICION", value = Posis.ToString() });
            dataenvio.filter.Add(new Elements { property = "ATTPARCIAL", value = Convert.ToByte(AttPar).ToString() });
            dataenvio.filter.Add(new Elements { property = "EtaProveedor", value = TiemEta });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            if (Convert.ToInt32(JRespuesta["status"]) == 200)
            {
                controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                controlFalla.message = JRespuesta["message"].ToString();
            }
            else
            {
                controlFalla.Solicitudes = new List<Solicitude>();
                controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                controlFalla.message = JRespuesta["message"].ToString();
            }
            return controlFalla;

        }

        public ControlFalla ConsultaGeneral(string cveEmp, int NumTicket, string FechaIni, string FechaFin)
        {
            if (NumTicket != 0)
            {
                FechaIni = null;
                FechaFin = null;
            }
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_Reparaciones_TEST";
            dataenvio.filter.Add(new Elements { property = "cveEmpresa", value = cveEmp });
            dataenvio.filter.Add(new Elements { property = "NumTicket", value = NumTicket.ToString() });
            dataenvio.filter.Add(new Elements { property = "FechaIni", value = FechaIni });
            dataenvio.filter.Add(new Elements { property = "FechaFin", value = FechaFin });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            data = JRespuesta["data"] as JArray;
            if (Convert.ToInt32(JRespuesta["status"]) == 200)
            {
                controlFalla = JsonConvert.DeserializeObject<ControlFalla>(data[0].ToString());
                controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                controlFalla.message = JRespuesta["message"].ToString();
            }
            else
            {
                controlFalla.Solicitudes = new List<Solicitude>();
                controlFalla.status = Convert.ToInt32(JRespuesta["status"]);
                controlFalla.message = JRespuesta["message"].ToString();
            }
            return controlFalla;
        }

    }
}
