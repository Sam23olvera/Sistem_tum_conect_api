namespace ConectDB.Models
{

    public class ControlFalla
    {
        public List<ListDataPrincipal> ListDataPrincipal { get; set; }
    }
    public class ListDataPrincipal
    {
        public int ClaveTicket { get; set; }
        public int ECO { get; set; }
        public DateTime? FechaAltaTicket { get; set; }
        public string CRCNOTIFICADO { get; set; }
        public string LLAMADAOPER { get; set; }
        public string MECANICAEMERGENCIA { get; set; }
        public string DIAGNOSTICO { get; set; }
        public string RCASIGNADO { get; set; }
        public string ETARC { get; set; }
        public string LLEGADARC { get; set; }
        public string ETAREPARACION { get; set; }
        public string CONCLUIDO { get; set; }
        public string USERGeneraTicket { get; set; }
        public string UNE { get; set; }
        public string TipoOperacion { get; set; }
        public string FallaInicial { get; set; }
        public DateTime? FechaCRCAsignado { get; set; }
        public string? UsrCRCAsignado { get; set; }
        public DateTime? FechaLlamadaOPE { get; set; }
        public DateTime? FechaMECEmergencia { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
    }

}
