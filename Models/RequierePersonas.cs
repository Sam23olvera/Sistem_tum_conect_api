using System.Text.RegularExpressions;

namespace ConectDB.Models
{
    public class RequierePersonas
    {
        private List<Puestos> puestonull = new List<Puestos>();
        public List<CatGenero> CatGenero { get; set; }
        public List<CatRequiereViajar> CatRequiereViajar { get; set; }
        public List<CatUNE>? CatUNE { get; set; }
        public List<Puestos> Puestos { get { return puestonull; } set { puestonull = value; } }
        public List<SubDepto> SubDepto { get; set; }
        public List<Depto> Deptos { get; set; }
        public List<Local> Localidad { get; set; }
        public List<Error> Errors { get; set; }

    }
    public class Local
    {
        public int ClaveArea { get; set; }
        public string Localidad { get; set; }
        public int ClaveDepartamento { get; set; }
    }

    public class Depto
    {
        public int ClaveDepartamento { get; set; }
        public string Departamento { get; set; }
        public int ClaveSubDepartamento { get; set; }
    }
    public class SubDepto
    {
        public int ClaveSubDepartamento { get; set; }
        public string Subdepto { get; set; }
        public int ClavePuesto { get; set; }
    }
    public class CatUNE
    {
        public int ClaveUnidadNegocio { get; set; }
        public string Descripcion { get; set; }
    }
    public class Puestos 
    {
        public int ClavePuesto { get; set; }
        public string? Puesto { get; set; }
    }
    public class CatRequiereViajar
    {
        public int ClaveRequiereViajar { get; set; }
        public string Descripcion { get; set; }
    }
    public class CatGenero
    {
        public int ClaveSexo { get; set; }
        public string Descripcion { get; set; }
    }
}
