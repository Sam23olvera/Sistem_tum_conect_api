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
            Joenvio = JObject.Parse("{\"data\": {\"bdCc\": 6,\"bdSch\": \"dbo\",\"bdSp\": \"SPQRY_CatRequisicion\" }, \"filter\": [ { \"property\": \"CveEmpresa\", \"value\": " + CveEmpresa + " } ] }");
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

        public JObject guardarReqPerson(RequierePersonas RQP)
        {
            int ClaveSexo = 0;
            if (RQP.sexo == "Masculino")
            {
                ClaveSexo = 1;
            }
            else if (RQP.sexo == "Femenino")
            {
                ClaveSexo = 2;
            }
            RootData dataenvio = new RootData();
            dataenvio.data.bdCc = 6;
            dataenvio.data.bdSch = "dbo";
            dataenvio.data.bdSp = "SPINS_RequiPersonal";
            dataenvio.filter.Add(new Elements { property = "ClaveArea", value = RQP.localidad.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveDepartamento", value = RQP.seledepa.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveEstado", value = RQP.residencia.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaverOrganigrama", value = RQP.seledepa.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClavePuesto", value = RQP.selnombrePuesto.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveUnidadNegocio", value = RQP.unidadNegocio.ToString() });
            dataenvio.filter.Add(new Elements { property = "VacantesCubrir", value = RQP.cantidadVacantes.ToString() });
            dataenvio.filter.Add(new Elements { property = "EdadMinima", value = RQP.Edadmin.ToString() });
            dataenvio.filter.Add(new Elements { property = "EdadMaxima", value = RQP.Edadmax.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveSexo", value = ClaveSexo.ToString() });
            dataenvio.filter.Add(new Elements { property = "ClaveRequiereViajar", value = RQP.selectviajar.ToString() });
            dataenvio.filter.Add(new Elements { property = "JefeInmediato", value = RQP.jdinmed.ToString() });
            dataenvio.filter.Add(new Elements { property = "Justificacion", value = RQP.justificacion.ToString() });
            dataenvio.filter.Add(new Elements { property = "ExperienciaNecesaria", value = RQP.experienciaNecesaria.ToString() });
            dataenvio.filter.Add(new Elements { property = "ExperienciaDeseable", value = RQP.experienciaDeseable.ToString() });
            dataenvio.filter.Add(new Elements { property = "Escolaridad", value = RQP.escolaridad.ToString() });
            dataenvio.filter.Add(new Elements { property = "Idiomas", value = RQP.idiomas.ToString() });
            dataenvio.filter.Add(new Elements { property = "DiasLaborablesde", value = RQP.LabDe.ToString() });
            dataenvio.filter.Add(new Elements { property = "DiasLaborablesa", value = RQP.LabA.ToString() });
            dataenvio.filter.Add(new Elements { property = "SueldoNetoLetra", value = RQP.sueldole.ToString() });
            dataenvio.filter.Add(new Elements { property = "SueldoNeto", value = RQP.Sueldonet.ToString() });
            dataenvio.filter.Add(new Elements { property = "Horariode", value = RQP.HorDe.ToString() });
            dataenvio.filter.Add(new Elements { property = "Horarioa", value = RQP.HorA.ToString() });
            dataenvio.filter.Add(new Elements { property = "FechaSolicitud", value = RQP.fechaSolicitud.ToString("yyyy-MM-dd HH:mm") });
            dataenvio.filter.Add(new Elements { property = "FirmaSolicita", value = RQP.solis.ToString() });
            JObject jconvert = JObject.Parse(JsonConvert.SerializeObject(dataenvio));

            JRespuesta = JObject.Parse(hh.HttpWebRequest("POST", url, jconvert));
            return JRespuesta;
        }
    }
}
