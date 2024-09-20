using ConectDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConectDB.DB
{
    public class CFVPretikDB
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        DataApi hh = new DataApi();
        CFVPretickDatum CFVPreticketDB = new CFVPretickDatum();
        JObject jsquerun = new JObject();
        JObject jsqueresponse = new JObject();
        JArray? data = null;
        public CFVPretickDatum ConsultaPreticket(DateTime Fech_cre,int cvemp) 
        {
            jsquerun = JObject.Parse("{\"data\": {\"bdCc\": 5,\"bdSch\": \"dbo\",\"bdSp\":\"SPQRY_PreTicket\"},\"filter\": [{\"property\": \"fechPretick\",\"value\": \"" + Fech_cre.ToString("yyyy-MM-dd HH:mm") + "\"},{\"property\": \"cvempresa\",\"value\":" + cvemp +"}]}");
            jsqueresponse = JObject.Parse(hh.HttpWebRequest("POST", url, jsquerun));
            data = jsqueresponse["data"] as JArray;
            if (Convert.ToInt16(jsqueresponse["status"]) == 200) 
            {
                CFVPreticketDB = JsonConvert.DeserializeObject<CFVPretickDatum>(data[0].ToString());
                CFVPreticketDB.Errors = new List<Error> { new Error { status = 200, message = jsqueresponse["message"].ToString() } };
            }
                return CFVPreticketDB;
        }
    }
}
