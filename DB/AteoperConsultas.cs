using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static ConectDB.Models.LogUser;
using System.Security.Policy;
using System;
using System.Collections.Generic;

namespace ConectDB.DB
{
    public class AteoperConsultas
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        DataApi hh = new DataApi();
        RootData dataenvio = new RootData();
        JObject jconvert = new JObject();
        JObject JRespuesta = new JObject();
        JArray data = new JArray();
        JArray databusca = new JArray();
        AtenOperador AtenOperador = new AtenOperador();
        List<CSAttOperador> vacioCSAttOperador = new List<CSAttOperador>();
        public AtenOperador Coatalgos(string empresa)
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_CatIniMantenimiento";
            dataenvio.filter.Clear();
            dataenvio.filter.Add(new Elements { property = "ClaveEmpresa", value = empresa });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            data = JRespuesta["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                AtenOperador.TBCAT_Dolly = JsonConvert.DeserializeObject<AtenOperador>(data[3].ToString()).TBCAT_Dolly;
                AtenOperador.TBCAT_TipoTicket = JsonConvert.DeserializeObject<AtenOperador>(data[3].ToString()).TBCAT_TipoTicket;
                AtenOperador.TBCAT_Operador = JsonConvert.DeserializeObject<AtenOperador>(data[2].ToString()).TBCAT_Operador;
                AtenOperador.TBCAT_Unidades = JsonConvert.DeserializeObject<AtenOperador>(data[0].ToString()).TBCAT_Unidades;
                AtenOperador.TBCAT_Remolques = JsonConvert.DeserializeObject<AtenOperador>(data[1].ToString()).TBCAT_Remolques;
                //controlFalla = JsonConvert.DeserializeObject<ControlFalla>(data[0].ToString());
            }
            return AtenOperador;
        }
        public AtenOperador Buscar(string empresa, string fechainicio, string fechafin)
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_AtencionOperador_TEST";
            dataenvio.filter.Add(new Elements { property = "cveEmpresa", value = empresa });
            dataenvio.filter.Add(new Elements { property = "FechaInicio", value = fechainicio });
            dataenvio.filter.Add(new Elements { property = "FechaFin", value = fechafin });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            databusca = JRespuesta["data"] as JArray;
            if (databusca != null && databusca.Count > 0)
            {
                AtenOperador = Coatalgos(empresa);
                AtenOperador.CSAttOperador = JsonConvert.DeserializeObject<AtenOperador>(databusca[0].ToString()).CSAttOperador;
                AtenOperador.Erroress = new List<Error> { new Error { status = 200, message = JRespuesta["message"].ToString() } };
            }
            else 
            {
                AtenOperador.CSAttOperador = vacioCSAttOperador;
                AtenOperador.Erroress = new List<Error> { new Error { status = 400, message = JRespuesta["status"].ToString() + JRespuesta["message"]?.ToString() } };
            }
            return AtenOperador;
        }

        public AtenOperador Guardar(AtenOperador registraroper,int Clvrepo) 
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPINS_TicketAtt_Operador";
            dataenvio.filter.Add(new Elements { property = "ClaveEquipoReportado", value = Clvrepo.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveOperador", value = registraroper.ClaveOperador.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveTipoTicket", value = registraroper.ClaveTipoTicket.ToString() });
            dataenvio.filter.Add(new Elements { property = "Observaciones", value = registraroper.Comentarios });
            dataenvio.filter.Add(new Elements { property = "FechaReporte", value = registraroper.FechaReporte.ToString("yyyy-MM-dd HH:mm:ss") });
            dataenvio.filter.Add(new Elements { property = "ClaveEmpresa", value = registraroper.cveEmp.ToString() });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            if (JRespuesta["status"].ToString() == "200") 
            {
                if (databusca != null && databusca.Count > 0)
                {
                    AtenOperador = Coatalgos(registraroper.cveEmp.ToString());
                    AtenOperador.CSAttOperador = JsonConvert.DeserializeObject<AtenOperador>(databusca[0].ToString()).CSAttOperador;
                    AtenOperador.Erroress = new List<Error> { new Error { status = Convert.ToInt32(JRespuesta["status"]), message = JRespuesta["message"].ToString() } };
                }
                else 
                {
                    AtenOperador = Coatalgos(registraroper.cveEmp.ToString());
                    AtenOperador.CSAttOperador = vacioCSAttOperador;
                    AtenOperador.Erroress = new List<Error> { new Error { status = Convert.ToInt32(JRespuesta["status"]), message = JRespuesta["message"].ToString() } };
                }
            }
            else
            {
                AtenOperador.CSAttOperador = vacioCSAttOperador;
                AtenOperador.Erroress = new List<Error> { new Error { status = 400, message = JRespuesta["status"].ToString() + JRespuesta["message"]?.ToString() } };
            }
            return AtenOperador;
        }
    }
}
