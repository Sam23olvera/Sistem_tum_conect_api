using System;
using System.ComponentModel.DataAnnotations;
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
        public List<Estado> Estados { get; set; }
        public List<Local> Localidad { get; set; }
        public List<Error> Errors { get; set; }

        [Display(Name = "Sueldo Neto Mensual")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? Sueldonet { get; set; } // Acepta nulos si es opcional
        public DateTime fechaSolicitud { get; set; }
        [Required(ErrorMessage = "Selecciona el Puesto")]
        public int selnombrePuesto { get; set; }
        public string? jefeInmediato { get; set; }
        public int subdept { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una cantidad válida de vacantes.")]
        public int cantidadVacantes { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un departamento.")]
        public int seledepa { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un departamento.")]
        public int unidadNegocio { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un localidad.")]
        public int localidad { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un menos de 200 caracteres.")]
        public string? justificacion { get; set; }
        public string? sexo { get; set; }
        [Range(18, 65, ErrorMessage = "La edad debe estar entre 18 y 65 años.")]
        public int Edadmin { get; set; }
        [Range(18, 65, ErrorMessage = "La edad debe estar entre 18 y 65 años.")]
        public int Edadmax{ get; set; }
        public int residencia{ get; set; }
        public string? experienciaNecesaria{ get; set; }
        public string? experienciaDeseable{ get; set; }
        public string? escolaridad{ get; set; }
        public string? idiomas{ get; set; }
        public string? LabDe{ get; set; }
        public string? LabA{ get; set; }
        public string? HorDe{ get; set; }
        public string? HorA{ get; set; }
        public int selectviajar{ get; set; }
        public string? sueldole{ get; set; } 
        public DateTime fechaRecibo { get; set; }
        public string? personaRecibe{ get; set; }
        public bool solis{ get; set; } 
        public bool jdinmed{ get; set; } 
        public bool chRH{ get; set; }
        public bool vice{ get; set; }
        public bool cal { get; set; }
    }
    public class Estado 
    {
        public int ClaveEstado { get; set; }
        public string Descripcion { get; set; }
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
