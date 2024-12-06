using NuGet.Protocol.Plugins;

namespace ConectDB.Models
{
    public class Ticket
    {
        public int CveEmpresa { get; set; }
        public int CveOrigTicket { get; set; }
        public int CveAccion { get; set; }
        public int CveUnidad { get; set; }
        public int CveOperador { get; set; }
        public int CveRemolque1 { get; set; }
        public int CveRemolque2 { get; set; }
        public int CveDolly { get; set; }
        public string CveUNE { get; set; }
        public int CveTipoOperacion { get; set; }
        public int CveTipoCarga { get; set; }
        public string TelOperador { get; set; }
        public int CveEstatus { get; set; }
        public string FechaUltPosGps { get; set; }
        public string DirPosGps { get; set; }
        public double LatGPS { get; set; }
        public double LonGPS { get; set; }
        public double LatNva { get; set; }
        public double LonNva { get; set; }
        public string Observ { get; set; }
        public int CvePreticket { get; set; }
        public bool ConViaje { get; set; }
        public int CveViajeTUM { get; set; }
        public List<Falla> Fallas { get; set; }
    }
    public class TicketFalla
    {
        public Ticket Ticket { get; set; }
    }
    public class Falla
    {
        public int CveOrigenFalla { get; set; }
        public int CveEquipo { get; set; }
        public int CveTipoClasifn { get; set; }
        public int CveTipoFalla { get; set; }
        public string? DescripFalla { get; set; }
        public string? DOT { get; set; }
        public string? Marca { get; set; }
        public string? MEDIDA { get; set; }
        public int POSICION { get; set; }
        public string? ECOLlanta { get; set; }
    }    
}
