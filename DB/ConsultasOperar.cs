using ConectDB.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Policy;

namespace ConectDB.DB
{
    public class ConsultasOperar
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        private DataApi conapi = new DataApi();
        private ConsulTipoOpera zTipoOper = new ConsulTipoOpera();

        public ConsulTipoOpera PrimeraCarga (int empresa) 
        {
            JObject JR1 = JObject.Parse(conapi.HttpWebRequest("POST", url, JObject.Parse("{\"data\":\r{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CatalogoTipoOperacion\"},\"filter\":[{\"property\":\"ClaveEmpresa\",\"value\":\"" + empresa + "\"}]}")));
            JArray JA1 = JR1["data"] as JArray;
            zTipoOper = JsonConvert.DeserializeObject<ConsulTipoOpera>(JA1[0].ToString());
            //JObject JR2 = JObject.Parse(conapi.HttpWebRequest("POST", url, JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CSTipoOperacion\"},\"filter\":[{\"property\":\"cveEmpresa\",\"value\": " + empresa + "},{\"property\":\"NumTicket\",\"value\": " + NumTicket + "},{\"property\":\"FechaIni\",\"value\":\""+ FechaIni + "\"},{\"property\":\"FechaFin\",\"value\": \"" + FechaFin + "\"},{\"property\":\"CveTipoOperacion\",\"value\":" + CveTipoOperacion + "}]}")));
            //JArray JA2 = JR2["data"] as JArray;
            //zTipoOper.CSxTipoOeracion = JsonConvert.DeserializeObject<ConsulTipoOpera>(JA2[0].ToString()).CSxTipoOeracion;
            return zTipoOper;
        }
        public ConsulTipoOpera SegundaCarga(int empresa, int NumTicket, string? FechaIni, string? FechaFin,int ClaveUnidadNegocio, int CveTipoOperacion,int clvEstatus,bool excel)
        {
            JObject JR1 = JObject.Parse(conapi.HttpWebRequest("POST", url, JObject.Parse("{\"data\":\r{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CatalogoTipoOperacion\"},\"filter\":[{\"property\":\"ClaveEmpresa\",\"value\":\"" + empresa + "\"}]}")));
            JArray JA1 = JR1["data"] as JArray;
            //pagina = (pagina - 1) * tamañomuestra;
            zTipoOper = JsonConvert.DeserializeObject<ConsulTipoOpera>(JA1[0].ToString());
            if (FechaIni == null)
            {
                FechaIni = "null ";
            }
            else 
            {
                FechaIni = "\"" + FechaIni + "\"";
            }
            if (FechaFin == null)
            {
                FechaFin = "null ";
            }
            else 
            {
                FechaFin = "\"" + FechaFin + "\"";
            }
            JObject JHR = JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CSTipoOperacion\"},\"filter\":[{\"property\":\"cveEmpresa\",\"value\": " + empresa + "},{\"property\":\"NumTicket\",\"value\": " + NumTicket + "},{\"property\":\"FechaIni\",\"value\":" + FechaIni + "},{\"property\":\"FechaFin\",\"value\": " + FechaFin + "},{\"property\":\"CveTipoOperacion\",\"value\":" + CveTipoOperacion + "},{\"property\":\"CveUnidadNegocio\",\"value\":" + ClaveUnidadNegocio + "},{\"property\":\"CveEstatus\",\"value\":" + clvEstatus + "}]}");
            JObject JR2 = JObject.Parse(conapi.HttpWebRequest("POST", url, JHR));
            if (JR2["status"].ToString() != "200")
            {
                List<CSxTipoOeracion> asgsg = new List<CSxTipoOeracion>();
                //zTipoOper.CSxTipoOeracion = asgsg;
                //zTipoOper.TotalSolicitudes = 0;
            }
            else 
            {
                if (excel == true) 
                {
                    JArray JA2 = JR2["data"] as JArray;
                    zTipoOper.CSxTipoOeracion = JsonConvert.DeserializeObject<ConsulTipoOpera>(JA2[0].ToString()).CSxTipoOeracion;
                }
                else 
                {
                    JArray JA2 = JR2["data"] as JArray;
                    if (JA2.Count != 0)
                    {
                        zTipoOper.CSxTipoOeracion = JsonConvert.DeserializeObject<ConsulTipoOpera>(JA2[0].ToString()).CSxTipoOeracion;
                        //zTipoOper.TotalSolicitudes = zTipoOper.CSxTipoOeracion.Count;
                        //zTipoOper.CSxTipoOeracion = zTipoOper.CSxTipoOeracion.Skip(pagina).Take(tamañomuestra).ToList();
                    }
                }
                
            }
            return zTipoOper;
        }
    }
}
