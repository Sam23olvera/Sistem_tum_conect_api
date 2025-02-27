namespace ConectDB.Models
{
    public class ConsulTipoOpera
    {
        private List<CSxTipoOeracion> cSxTipoOeraciones = new List<CSxTipoOeracion>();
        public List<TBCATTipoOp>? TBCAT_TipoOp { get; set; }
        public List<TBCATUNE>? TBCAT_UNE { get; set; }
        public List<TBCATEstatus>? TBCAT_Estatus { get; set; }
        public List<CSxTipoOeracion> CSxTipoOeracion { get { return cSxTipoOeraciones; } set { cSxTipoOeraciones = value; } }
        public int? TotalSolicitudes { get; set; }
    }
    public class TBCATEstatus
    {
        public int ClaveEstatus { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
    public class TBCATTipoOp
    {
        public int ClaveTipoOperacion { get; set; }
        public string? Descripcion { get; set; }
        public string? Mascara_Rainde { get; set; }
        public int CveUnidadNegocio { get; set; }
        public int ClaveSalario { get; set; }
    }
    public class TBCATUNE
    {
        public int ClaveUnidadNegocio { get; set; }
        public string? Descripcion { get; set; }
        public string? Mascara_Rainde { get; set; }
    }
    public class CSxTipoOeracion
    {
        public string? UNE { get; set; }
        public string? TipoOperacion { get; set; }
        public int ClaveEstatus { get; set; }
        public string? Estatus { get; set; }
        public DateTime? Alta { get; set; }
        public DateTime? Asignado { get; set; }
        public string UsuarioAsignado { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? TimerReparacion { get; set; }
        public int NumTicket { get; set; }
        public string? TipoTicket { get; set; }
        public string? Equipo { get; set; }
        public string? TipoFalla { get; set; }
        public string? NotasMonitoreo { get; set; }
        public string? Diesel { get; set; }
        public string? Grua { get; set; }
        public string? AtencionParcial { get; set; }
        public int ClaveOperador { get; set; }
        public string? NombOperador { get; set; }
        public string? Tel_Operador { get; set; }
        public string? Ruta { get; set; }
    }
}
