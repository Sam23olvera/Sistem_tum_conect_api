
namespace ConectDB.Models
{
    public class CFVPretickDatum
    {
        private List<CFVPretick> cFVPreticks = new List<CFVPretick>();
        private List<Error> Erro = new List<Error>();
        public List<CFVPretick> CFVPretick { get { return cFVPreticks; } set { cFVPreticks = value; } }
        public List<Error> Errors { get { return Erro; } set { Erro = value; } }
    }
    public class CFVPretick
    {
        public int ClavePreTicket { get; set; }
        public int NumOperador { get; set; }
        public int Unidad { get; set; }
        public string? Remolque { get; set; }
        public string? Dolly { get; set; }
        public string? Tipo_Falla { get; set; }
        public string? Tipo_clasificacion { get; set; }
        public DateTime FechaEntroPatio { get; set; }
        public DateTime FechaCreaPreTicket { get; set; }
        public string? UsuarioCreoPreticket { get; set; }
        public int NumTicket { get; set; }
        public string? UserTicket { get; set; }
        public string? Estatus { get; set; }
        public string? observaciones { get; set; }
    }
}
