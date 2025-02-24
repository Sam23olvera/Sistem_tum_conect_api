namespace ConectDB.Models
{
    public class SPINSCandidatoAE
    {
        public int IdTipoTrab { get; set; }
        public string Apaterno { get; set; }
        public string AMaterno { get; set; }
        public string Nombres { get; set; }
        public int IdPais { get; set; }
        public int IdEstado { get; set; }
        public int IdMpio { get; set; }
        public int IdColonia { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string CP { get; set; }
        public string TCel { get; set; }
        public string TLocal { get; set; }
        public string FechaNac { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string NSS { get; set; }
        public string Genero { get; set; }
        public int IdEscolar { get; set; }
        public bool Concluida { get; set; }
        public bool Reingreso { get; set; }
        public int IdTipoLicen { get; set; }
        public string NumLicen { get; set; }
        public string VigenciaLicen { get; set; }
        public string RControl { get; set; }
        public bool ConInfonavit { get; set; }
        public string FolInfonavit { get; set; }
        public string Experiencia { get; set; }
        public bool ConFonacot { get; set; }
        public string FolFonacot { get; set; }
        public int IdTipoOp { get; set; }
        public int IdZonaTrab { get; set; }
        public int IdBanco { get; set; }
        public string CuentaBanca { get; set; }
        public int IdPuesto { get; set; }
        public int ClaveSalario { get; set; }
        public int AnoExperiencia { get; set; }
        public string VigenciaMedic { get; set; }
        public int ClaveOrigi { get; set; }
        public int EdoCivil { get; set; }
        public List<TiposUni> TiposUnis { get; set; }
        public List<Docto> Doctos { get; set; }
    }
    public class Docto
    {
        public int IdDoctoRecibe { get; set; }
    }
    public class TiposUni
    {
        public int IdTipoUnidadOp { get; set; }
    }
}
