namespace ConectDB.Models
{
    public class CuentaCabezas
    {
        public List<HeadCount> HeadCount { get; set; }
    }
    public class HeadCount
    {
        public int? ClaveHeadCount { get; set; }
        public string? Estatus { get; set; }
        public string? Puesto { get; set; }
        public string? AgenciaBase { get; set; }
        public string? TipoPersona { get; set; }
        public int? NumEmpleado { get; set; }
        public string? Empleado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Direccion { get; set; }
        public string? RFC { get; set; }
        public string? CURP { get; set; }
        public string? IMSS { get; set; }
        public bool Sustituible { get; set; }
        public string? TipoOperacion { get; set; }
        public int? ClaveNivel { get; set; }
        public int? ClaveEmpleado { get; set; }
    }
}
