using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ConectDB.Models
{
    public class AltaExpressModel: IValidatableObject
    {

        [Required(ErrorMessage = "Debes seleccionar un Tipo de Trabajador")]
        public int CveTipoEmp { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una Escolaridad")]
        public int Escol { get; set; }
        [Required(ErrorMessage = "Apellido Paterno es obligatorio")]
        public string ApPaterno { get; set; }

        [Required(ErrorMessage = "Apellido Materno es obligatorio")]
        public string ApMaterno { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Calle es obligatorio")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una Colonia")]
        public int Colonia { get; set; }

        [Required(ErrorMessage = "Debes de ingresar el CP")]
        public string CP { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un País")]
        public int SelPais { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un Estado")]
        public int selEstado { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un Municipio")]
        public int seleMuni { get; set; }

        [Required(ErrorMessage = "Ingresa el numero exterior")]
        public string NumExterior { get; set; }
        public string NumInterior { get; set; }

        [Required(ErrorMessage = "Debes de Marcar el Genero")]
        public string sexo { get; set; }
        public int originario { get; set; }
        [Required(ErrorMessage = "Debes de Agregar un celular")] 
        public string Cel { get; set; }
        [Required(ErrorMessage = "Debes de Agregar un telefono")]
        public string Tel { get; set; }
        [Required(ErrorMessage = "Debes de colocar la Fecha de Nacimiento")]
        public string FechNac { get; set; }
        [Required(ErrorMessage = "Debes de colocar el CURP")]
        public string CURP { get; set; }
        [Required(ErrorMessage = "Debes de colocar el RFC")]
        public string RFC { get; set; }
        [Required(ErrorMessage = "Debes de colocar el Numero de Seguro Social")]
        public string NSS { get; set; }
        public bool EscoConcluida { get; set; }
        [Required(ErrorMessage = "Debes de Selecionar el Estado Civil")]
        public int EdoCivil { get; set; }
        public bool Reingreso { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un Tipo de Licencia")]
        public int SeleLic { get; set; }
        [Required(ErrorMessage = "Ingresa el Numero de Licencia")]
        public string NumLicen { get; set; }
        [Required(ErrorMessage = "Ingresa la Vigencia")]
        public string VigenciaLicen { get; set; }
        [Required(ErrorMessage = "Debes de Ingresar el RControl")]
        public string RControl { get; set; }
        public int AnoExperiencia { get; set; }
        public bool ConInfonavit { get; set; }
        public string FolInfonavit { get; set; }
        public bool ConFonacot { get; set; }
        public string FolFonacot { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un Tipo de Operación")]
        public int TipOpera { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una Zona de Trabajo")]
        public int ZonTra { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un Banco")]
        public int Banc { get; set; }

        [Required(ErrorMessage = "Ingresa un Numero de Cuenta")]
        public string CuentaBanca { get; set; }
        [Required(ErrorMessage = "Cuentanos tu Experiencia")]
        public string Experiencia { get; set;}
        [Required(ErrorMessage = "Debe seleccionar un Puesto")]
        public int selePues { get; set; }
        [Required(ErrorMessage = "Debes de colocar la Fecha de Vigencia Apto Medico")]
        public string AptMedi { get; set; }
        //[Required(ErrorMessage = "Debes seleccionar un Esquema de Pago")]
        //public int SelEsquemPago { get; set; }

        public int selSal { get; set; }
        public bool RangoMedio { get; set; }
        public bool Thorton { get; set; }
        public bool Rabon { get; set; }
        public bool Camioneta { get; set; }
        public bool TractoSenci { get; set; }
        public bool VHlig { get; set; }
        public bool TracFull { get; set; }
        
        public List<TBCATTipoOp> TBCAT_TipoOp { get; set; }
        public List<TBCATTipoTrabajador> TBCAT_TipoTrabajador { get; set; }
        public List<TBCATEscolaridad> TBCAT_Escolaridad { get; set; }
        public List<TBCATTipoLicencium> TBCAT_TipoLicencia { get; set; }
        public List<TBCATZonaTrabajo> TBCAT_ZonaTrabajo { get; set; }
        public List<TBCATPuestosOperativo> TBCAT_PuestosOperativos { get; set; }
        public List<TBCATDoctoRecibido> TBCAT_DoctoRecibidos { get; set; }
        public List<TBCATBanco> TBCAT_Bancos { get; set; }
        public List<TBCATEsquemaPago> TBCAT_EsquemaPago { get; set; }
        public List<TBCATPais> TBCAT_Pais { get; set; }
        public List<TBCATEstadoCivil> TBCAT_EstadoCivil { get; set; }
        public List<TBCATEstado> TBCAT_Estado { get; set; }
        public List<TBCATOriginario> TBCAT_OriginarioDe { get; set; }
        public List<Error> Errors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(RangoMedio || Thorton || Rabon || Camioneta || TractoSenci || VHlig || TracFull))
            {
                yield return new ValidationResult(
                    "Debes seleccionar al menos un tipo de vehículo en el que tengas experiencia.",
                    new[] { nameof(Thorton), nameof(Rabon), nameof(Camioneta), nameof(TractoSenci), nameof(VHlig), nameof(TracFull) }
                );
            }
        }

    }
    public class TBCATEstadoCivil
    {
        public int ClaveEstaCivil { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATOriginario
    {
        public string Entidad { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATPais {
        public int ClavePais { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATEstado
    {
        public int ClaveEstado { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATEsquemaPago
    {
        public int ClaveEsquemaPago { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATDoctoRecibido
    {
        public int ClaveDoctoRecibe { get; set; }
        public string Descripcion { get; set; }
        public bool IncluirEvidencia { get; set; }
    }

    public class TBCATPuestosOperativo
    {
        public int ClavePuesto { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATZonaTrabajo
    {
        public int ClaveZonTra { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATTipoLicencium
    {
        public int CveTipoLic { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATTipoTrabajador
    {
        public int CveTipoEmp { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATEscolaridad
    {
        public int CveEscolaridad { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATBanco
    {
        public int ClaveBanco { get; set; }
        public string Descripcion { get; set; }
    }
    
}
