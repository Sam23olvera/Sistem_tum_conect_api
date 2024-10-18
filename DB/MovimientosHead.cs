using ConectDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using static ConectDB.Models.LogUser;

namespace ConectDB.DB
{
    public class MovimientosHead
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        JArray? data = null;
        CuentaCabezas cabezas = new CuentaCabezas();
        DataApi hh = new DataApi();
        JObject Envio = new JObject();
        JObject json = new JObject();

        public JObject Promociones(int claveEmpleado,int Puestos,string txtObaservaProm, int idus) 
        {
            string jsonEnvio = "{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPINS_HC_PostulantesInternos\" }, \"filter\": [ { \"property\": \"claveEmpleado\",\"value\":" + claveEmpleado + " },{ \"property\": \"Puestos\",\"value\": \"" + Puestos + "\"}, { \"property\": \"Observaciones\" ,\"value\": \"" + txtObaservaProm + "\" } ] }";
            json = JObject.Parse(hh.HttpWebRequest("POST", url, Envio));
            return json;
        }
        public JObject MovimientoBaja(int editClaveEmpleado, int editClaveHeadCountBaja, int idCausa, DateTime FechaBaja, bool RecContra, string txtObaserva, int idus)
        {
            string jsonEnvio = "{'Bajas':[";
            jsonEnvio += "{";
            jsonEnvio += "'ClaveHeadCount': " + editClaveHeadCountBaja + ",";
            jsonEnvio += "'ClaveEmpleado': " + editClaveEmpleado + ",";
            jsonEnvio += "'ClaveCausaBaja': " + idCausa + ",";
            jsonEnvio += "'Recontratable': '" + RecContra + "',";
            jsonEnvio += "'FechaBaja': '" + FechaBaja.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            jsonEnvio += "'Observaciones': '" + txtObaserva + "' ";
            jsonEnvio += "}";
            jsonEnvio += "] }";

            Envio = JObject.Parse("{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPUPD_HC_Baja\" }, \"filter\": [ { \"property\": \"Json1\", \"value\":\"" + jsonEnvio + "\" },{ \"property\": \"claveUsuario\",\"value\":" + idus + "} ] }");
            json = JObject.Parse(hh.HttpWebRequest("POST", url, Envio));
            return json;

        }

        public CuentaCabezas TraerCabeza()
        {
            Envio = JObject.Parse("{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPQRY_HeadCount\"\r\n  },\"filter\":[  ]}");
            json = JObject.Parse(hh.HttpWebRequest("POST", url, Envio));
            data = json["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                cabezas.HeadCount = JsonConvert.DeserializeObject<CuentaCabezas>(data[0].ToString()).HeadCount;
            }
            return cabezas;
        }

    }
}
