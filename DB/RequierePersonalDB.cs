using ConectDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ConectDB.Models.LogUser;

namespace ConectDB.DB
{
    public class RequierePersonalDB
    {
        private string url = "https://webportal.tum.com.mx/wsstmdv/api/execsp";
        DataApi hh = new DataApi();
        JObject Joenvio = new JObject();
        JObject JRespuesta = new JObject();
        JArray? data = null;
        RequierePersonas regreso = new RequierePersonas();
        public RequierePersonas traerCatalogos(int CveEmpresa) 
        {
            Joenvio = JObject.Parse("{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPQRY_CatRequisicion\" }, \"filter\": [ { \"property\": \"CveEmpresa\", \"value\": "+ CveEmpresa + " } ] }");
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, Joenvio));
            data = JRespuesta["data"] as JArray;
            if (Convert.ToInt16(JRespuesta["status"]) == 200)
            {
                regreso = JsonConvert.DeserializeObject<RequierePersonas>(data[0].ToString());
                regreso.Errors = new List<Error> { new Error { status = 200, message = JRespuesta["message"].ToString() } };
            }
            else if (Convert.ToInt16(JRespuesta["status"]) == 400)
            {
                regreso.Errors = new List<Error> { new Error { status = 400, message = JRespuesta["message"].ToString() + "No hay Datos" } };
            }
            else 
            {
                regreso.Errors = new List<Error> { new Error { status = Convert.ToInt16(JRespuesta["status"]), message = JRespuesta["message"].ToString() } };
            }
            return regreso;
        }
    }
}
