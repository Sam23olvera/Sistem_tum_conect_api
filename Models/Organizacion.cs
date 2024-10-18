namespace ConectDB.Models
{
    public class Organizacion
    {
        public List<Organigrama> Organigrama { get; set; }

    }
    public class Organigrama
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string? Area { get; set; }
        public string? Plazas { get; set; }
        public string? Photo { get; set; }
        public string? Agencia { get; set; }
        public string? Departamento { get; set; }

    }
}
