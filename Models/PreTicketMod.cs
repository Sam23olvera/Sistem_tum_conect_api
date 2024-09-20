using ConectDB.DB;

namespace ConectDB.Models
{

    public class PreTicketMod
    {
        private List<TBCATTipoTicket> tipticklist = new List<TBCATTipoTicket>();
        private List<PreTick> preTicks = new List<PreTick>();
        public List<PreTick> PreTickets { get { return preTicks; } set { preTicks = value; } }
        public List<TBCATTipoTicket> TBCAT_TipoTicket { get { return tipticklist; } set { tipticklist = value; } }
        public List<Error> Errores { get; set; }
    }

    public class PreTick
    {
        public int ClavePreTicket { get; set; }
        public string? NameEmpresa { get; set; }
        public string? NameOpe { get; set; }
        public int Unidad { get; set; }
        public string? Remolque { get; set; }
        public string? NumeroDolly { get; set; }
        public bool Cargado { get; set; }
        public string? TipoFalla { get; set; }
        public string? TipoClasificacion { get; set; }
        public string? Tel_Operador { get; set; }
        public DateTime FechaUltPosicion { get; set; }
        public string? UltimaPosicionGps { get; set; }
        public string? ObsOperacion { get; set; }
        public DateTime FechaEntroPatio { get; set; }
        public DateTime FechaCreacionPreticket { get; set; }
        public string? Usuario { get; set; }
        public string? TipoTicket { get; set; }
        public string? DOT { get; set; }
        public string? MARCA { get; set; }
        public string? MEDIDA { get; set; }
        public int POSICION { get; set; }
    }
}
