using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Policy;
using System;

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
        TicketFalla ticket = new TicketFalla();
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
                return zcioFallas;
            }
        }

        public ModelFallas Guardar(ModelFallas fallas)
        {
            try
            {

                ticket.Ticket.CveEmpresa = fallas.cveEmp;
                ticket.Ticket.CveOrigTicket = fallas.selorigen;
                ticket.Ticket.CveAccion = fallas.selAccion;
                ticket.Ticket.CveUnidad = fallas.seleuni;
                ticket.Ticket.CveOperador = fallas.Opera;
                ticket.Ticket.CveRemolque1 = fallas.remolque1;
                ticket.Ticket.CveRemolque2 = fallas.remolque2;
                ticket.Ticket.CveDolly = fallas.selDolly;
                ticket.Ticket.CveTipoOperacion = fallas.SelectOperacion;
                ticket.Ticket.CveTipoCarga = fallas.selTipCarga;
                ticket.Ticket.TelOperador = fallas.telopera;
                ticket.Ticket.CveEstatus = 1;
                ticket.Ticket.FechaUltPosGps = fallas.fechaGPs;
                if (fallas.DirPosGps == "undefined")
                {
                    fallas.DirPosGps = "";
                }
                ticket.Ticket.DirPosGps = fallas.DirPosGps;
                ticket.Ticket.LatGPS = fallas.latitud;
                ticket.Ticket.LonGPS = fallas.longitud;
                //ticket.Ticket.LatNva
                //ticket.Ticket.LonNva
                //ticket.Ticket.Observ
                //ticket.Ticket.CvePreticket
                //ticket.Ticket.ConViaje
                //ticket.Ticket.CveViajeTUM

                dataenvio.data.bdCc = 5;
                dataenvio.data.bdSch = "dbo";
                dataenvio.data.bdSp = "SPINS_ControlTicket";
                dataenvio.filter.Clear();
                //dataenvio.filter.Add(new Elements { property = "Dat1", value =  });
                return zcioFallas;
            }
            catch (Exception e)
            {
                return zcioFallas;
            }
        }
    }
}
