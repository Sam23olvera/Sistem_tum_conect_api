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
        public ControlFalla PrimerCarga_sin_catlog(string fechaIni,string fechfin)
        {
            dataenvio.data.bdCc = 5;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPQRY_DataTableFC";
            dataenvio.filter.Add(new Elements { property = "FechaInicial", value = fechaIni });
            dataenvio.filter.Add(new Elements { property = "FechaFinal", value = fechfin });
            jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));
            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            data = JRespuesta["data"] as JArray;
            if (data != null && data.Count > 0)
            {
                controlFalla.ListDataPrincipal = JsonConvert.DeserializeObject<ControlFalla>(data[0].ToString()).ListDataPrincipal;
            }
            return controlFalla;
        }
    }
}
