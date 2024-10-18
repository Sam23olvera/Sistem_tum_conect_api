using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConectDB.Models;

namespace ConectDB.DB
{
    public class OrganigramaDB
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        private readonly DataApi hh = new DataApi();
        private Organizacion zOrganigrama = new Organizacion();
        JObject json = new JObject();
        JArray? data = new JArray();

        public Organizacion Mapaorganizacion()
        {
            JObject JR1 = JObject.Parse("{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPQRY_Organigrama1\" },\"filter\":[] }");
            json = JObject.Parse(hh.HttpWebRequest("POST", url, JR1));
            data = json["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                zOrganigrama.Organigrama = JsonConvert.DeserializeObject<Organizacion>(data![0].ToString()).Organigrama;
            }
            return zOrganigrama;
        }
    }
}
