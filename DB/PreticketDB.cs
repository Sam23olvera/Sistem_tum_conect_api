using ConectDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.DB
{
    public class PreticketDB
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        DataApi hh = new DataApi();
        PreTicketMod preTicket = new PreTicketMod();
        JObject jsCata = new JObject();
        JObject jsdat = new JObject();
        JArray? data = null;
        JArray? Cata = null;
        JObject js = new JObject();
        JObject jsCat = new JObject();

        public PreTicketMod ConsultaPreticket(int CVEM, string? fech, int Clavtick)
        {
            jsCata = JObject.Parse("{\"data\":{\"bdCc\": 5,\"bdSch\":\"dbo\",\"bdSp\": \"SPQRY_CAT_TipoTicket\"}}");
            jsCat = JObject.Parse(hh.HttpWebRequest("POST", url, jsCata));
            Cata = jsCat["data"] as JArray;
            preTicket.TBCAT_TipoTicket = JsonConvert.DeserializeObject<PreTicketMod>(Cata[0].ToString()).TBCAT_TipoTicket;
            if (fech == null)
            {
                jsdat = JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CSPreticket\"},\"filter\":[{\"property\":\"CVEM\",\"value\":" + CVEM + "},{\"property\":\"FechCreTick\",\"value\":null},{\"property\":\"ClaveTipoTicket\",\"value\":" + Clavtick + "}]}");
            }
            else
            {
                jsdat = JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CSPreticket\"},\"filter\":[{\"property\":\"CVEM\",\"value\":" + CVEM + "},{\"property\":\"FechCreTick\",\"value\":\"" + fech + " 23:59:59" + "\"},{\"property\":\"ClaveTipoTicket\",\"value\":" + Clavtick + "}]}");
            }
            js = JObject.Parse(hh.HttpWebRequest("POST", url, jsdat));
            if (Convert.ToInt16(js["status"]) == 200)
            {
                data = js["data"] as JArray;
                preTicket.PreTickets = JsonConvert.DeserializeObject<PreTicketMod>(data[0].ToString()).PreTickets;
                preTicket.Errores = new List<Error> { new Error { status = 200, message = "Exitoso" } };
            }
            else if (Convert.ToInt16(js["status"]) == 400)
            {
                preTicket.Errores = new List<Error> { new Error { status = 400, message = "No se Encuentran Datos de la fecha:" + fech } };
            }
            return preTicket;
        }

        public PreTicketMod CreaTick(string[] clavetick, int CVEM, int origTick, int user)
        {
            jsCata = JObject.Parse("{\"data\":{\"bdCc\": 5,\"bdSch\":\"dbo\",\"bdSp\": \"SPQRY_CAT_TipoTicket\"}}");
            jsCat = JObject.Parse(hh.HttpWebRequest("POST", url, jsCata));
            Cata = jsCat["data"] as JArray;
            preTicket.TBCAT_TipoTicket = JsonConvert.DeserializeObject<PreTicketMod>(Cata[0].ToString()).TBCAT_TipoTicket;
            string jsonEnvio = "";
            if (clavetick.Length == 1)
            {
                jsonEnvio = "{'RegiPretick':[";
                jsonEnvio += "{ ";
                jsonEnvio += "'claveEmpresa' : " + CVEM + ", ";
                jsonEnvio += "'Pretick':" + clavetick[0] + ", ";
                jsonEnvio += "'ClaveOrigenTicket': " + origTick + ", ";
                jsonEnvio += "'Usuario': " + user;
                jsonEnvio += " }";
                jsonEnvio += "]}";
            }
            else 
            {
                jsonEnvio = "{'RegiPretick':[";
                for (int i = 0; i < clavetick.Length; i++)
                {
                    jsonEnvio += "{ ";
                    jsonEnvio += "'claveEmpresa' : " + CVEM + ", ";
                    jsonEnvio += "'Pretick':" + clavetick[i] + ", ";
                    jsonEnvio += "'ClaveOrigenTicket': " + origTick + ", ";
                    jsonEnvio += "'Usuario': " + user;
                    jsonEnvio += " },";
                }
                jsonEnvio += "]}";
                jsonEnvio = jsonEnvio.Replace(",]}", "]}").Replace("\\", "");
            }
            JObject respuestJS = GuardarPreTick(jsonEnvio);
            if (respuestJS["status"].ToString() == "200")
            {
                jsdat = JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPQRY_CSPreticket\"},\"filter\":[{\"property\":\"CVEM\",\"value\":" + CVEM + "},{\"property\":\"FechCreTick\",\"value\":null},{\"property\":\"ClaveTipoTicket\",\"value\":" + 0 + "}]}");
                js = JObject.Parse(hh.HttpWebRequest("POST", url, jsdat));
                data = js["data"] as JArray;
                if (Convert.ToInt16(js["status"]) == 200)
                {
                    preTicket.PreTickets = JsonConvert.DeserializeObject<PreTicketMod>(data[0].ToString()).PreTickets;
                    preTicket.Errores = new List<Error> { new Error { status = 200, message = respuestJS["message"].ToString() } };
                }
                else if (data.Count == 0)
                {
                    preTicket.Errores = new List<Error> { new Error { status = 200, message = "No se encuentran Datos con el Preticket:"+ respuestJS["message"].ToString() } };
                }
                else { 
                    preTicket.Errores = new List<Error> { new Error { status = 400, message = "Error" + respuestJS["status"].ToString() + " " + respuestJS["message"].ToString() } };
                }
            }
            else
            {
                preTicket.Errores = new List<Error> { new Error { status = 400, message = "Error" + respuestJS["status"].ToString() + " " + respuestJS["message"].ToString() } };
            }
            return preTicket;
        }
        private JObject GuardarPreTick(string jsonEnvio) 
        {
            try 
            {
                JObject jt = JObject.Parse("{\"data\":{\"bdCc\":5,\"bdSch\":\"dbo\",\"bdSp\":\"SPINS_ConvertPreTicket_Ticket\"},\"filter\":[{\"property\": \"Json1\",\"value\":\"" + jsonEnvio + "\"}]}");
                JObject je = JObject.Parse(hh.HttpWebRequest("POST", url, jt));
                if (je["status"].ToString() == "400")
                {
                    return je;
                }
                return je;

            }
            catch (Exception e)
            {
                return JObject.Parse("{ \"status\": \"Desconosido\",\"message\":\"" + e.Message.ToString() + "\"}");
            }

        }
    }
}
