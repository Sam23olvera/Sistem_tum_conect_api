using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ConectDB.Models
{
    public class AtenOperador
    {
        private List<TBCATOperador> tBCATOperadors = new List<TBCATOperador>();
        private List<CSAttOperador> Castop = new List<CSAttOperador>();
        private List<TBCATTipoTicket> tipoTickets = new List<TBCATTipoTicket>();
        private List<TBCATUnidade> unidadvacia = new List<TBCATUnidade>();
        private List<TBCAT_Dolly_S> dolyvaico = new List<TBCAT_Dolly_S>();
        private List<TBCATRemolque> remolvacio = new List<TBCATRemolque>();

        public List<TBCATOperador> TBCAT_Operador { get { return tBCATOperadors; } set { tBCATOperadors = value; } }
        public List<TBCATTipoTicket> TBCAT_TipoTicket { get { return tipoTickets; } set { tipoTickets = value; } }
        public List<TBCATUnidade> TBCAT_Unidades { get { return unidadvacia; } set { unidadvacia = value; } }
        public List<TBCAT_Dolly_S> TBCAT_Dolly { get { return dolyvaico; } set { dolyvaico = value; } }
        public List<TBCATRemolque> TBCAT_Remolques { get { return remolvacio; } set { remolvacio = value; } }
        public List<Error> Erroress { get; set; }
        public List<CSAttOperador> CSAttOperador { get { return Castop; } set { Castop = value; } }

        [Required(ErrorMessage = "Selecciona el un tipo de Ticket")]
        public int ClaveTipoTicket { get; set; }

        [Required(ErrorMessage = "Selecciona el un tipo de operador")]
        public int ClaveOperador { get; set; }
        [Required(ErrorMessage = "Selecciona el un tipo de operador")]
        public DateTime FechaReporte { get; set; }
        public string Comentarios { get; set; }
        public int cveEmp { get; set; }
    }
    public class CSAttOperador
    {
        public int ClaveReporteOperador { get; set; }
        public int TICKET { get; set; }
        public string Equipo { get; set; }
        public int NumOperador { get; set; }
        public string NombreOperador { get; set; }
        public string TipoTicket { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaReporte { get; set; }
    }
    public class TicketAttOp
    {
        public int ClaveEquipoReportado { get; set; }
        public int ClaveOperador { get; set; }
        public int ClaveTipoTicket { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaReporte { get; set; }
        public int ClaveEmpresa { get; set; }
    }
}
