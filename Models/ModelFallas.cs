using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ConectDB.Models
{

    public class ModelFallas
    {
        private List<TBCATUnidade> zTBCAT_Unidades = new List<TBCATUnidade>();
        private List<TBCATRemolque> zTBCATRemolque = new List<TBCATRemolque>();
        private List<TBCATOperador> zTBCAT_Operador = new List<TBCATOperador>();
        private List<TBCATSeleccionEquipo> zTBCATSeleccionEquipo = new List<TBCATSeleccionEquipo>();
        private List<TBCATTipoApoyo> zTBCAT_TipoApoyo = new List<TBCATTipoApoyo>();
        private List<TBCATTipoTicket> zTBCAT_TipoTicket = new List<TBCATTipoTicket>();
        private List<TBCATTipoClasificacion> zTBCATTipoClasificacion = new List<TBCATTipoClasificacion>();
        private List<TBCATTipoCarga> zTBCAT_TipoCarga = new List<TBCATTipoCarga>();
        private List<TBCATTipoFalla> zTBCAT_TipoFalla = new List<TBCATTipoFalla>();
        private List<TBCATTipoEquipo> zTBCAT_TipoEquipo = new List<TBCATTipoEquipo>();
        private List<TBCAT_Dolly_S> zVaciTBCAT_Dolly_S = new List<TBCAT_Dolly_S>();
        private List<TBCATOrigenTicket> zTBCAT_OrigenTicket = new List<TBCATOrigenTicket>();
        private List<TBCATTipoMtto> zTBCAT_TipoMtto = new List<TBCATTipoMtto>();
        private List<UltimaPosicion> zUltimaPosicion = new List<UltimaPosicion>();
        private List<TBCATTipoOp> zTBCAT_TipoOp = new List<TBCATTipoOp>();

        public List<TBCATUnidade> TBCAT_Unidades { get { return zTBCAT_Unidades; } set { zTBCAT_Unidades = value; } }
        public List<TBCATRemolque> TBCAT_Remolques { get { return zTBCATRemolque; } set { zTBCATRemolque = value; } }
        public List<TBCATOperador> TBCAT_Operador { get { return zTBCAT_Operador; } set { zTBCAT_Operador = value; } }
        public List<TBCATSeleccionEquipo> TBCAT_SeleccionEquipo { get { return zTBCATSeleccionEquipo; } set { zTBCATSeleccionEquipo = value; } }
        public List<TBCATTipoApoyo> TBCAT_TipoApoyo { get { return zTBCAT_TipoApoyo; } set { zTBCAT_TipoApoyo = value; } }
        public List<TBCATTipoTicket> TBCAT_TipoTicket { get { return zTBCAT_TipoTicket; } set { zTBCAT_TipoTicket = value; } }
        public List<TBCATTipoClasificacion> TBCAT_TipoClasificacion { get { return zTBCATTipoClasificacion; } set { zTBCATTipoClasificacion = value; } }
        public List<TBCATTipoCarga> TBCAT_TipoCarga { get { return zTBCAT_TipoCarga; } set { zTBCAT_TipoCarga = value; } }
        public List<TBCATTipoFalla> TBCAT_TipoFalla { get { return zTBCAT_TipoFalla; } set { zTBCAT_TipoFalla = value; } }
        public List<TBCATTipoEquipo> TBCAT_TipoEquipo { get { return zTBCAT_TipoEquipo; } set { zTBCAT_TipoEquipo = value; } }
        public List<TBCAT_Dolly_S> TBCAT_Dolly { get { return zVaciTBCAT_Dolly_S; } set { zVaciTBCAT_Dolly_S = value; } }
        public List<TBCATOrigenTicket> TBCAT_OrigenTicket { get { return zTBCAT_OrigenTicket; } set { zTBCAT_OrigenTicket = value; } }
        public List<TBCATTipoMtto> TBCAT_TipoMtto { get { return zTBCAT_TipoMtto; } set { zTBCAT_TipoMtto = value; } }
        public List<TBCATTipoOp> TBCAT_TipoOp { get { return zTBCAT_TipoOp; } set { zTBCAT_TipoOp = value; } }
        public List<UltimaPosicion> UltimaPosicion { get { return zUltimaPosicion; } set { zUltimaPosicion = value; } }

        public int selAccion { get; set; }
        public int selorigen { get; set; }
        public bool inCheckViaje { get; set; }
        public int selTipCarga { get; set; }
        public string fechaGPs { get; set; }
        public string fechaGPsNew { get; set; }
        public string DirPosGps { get; set; }
        public string DirPosGpsNew { get; set; }
        public double latitud { get; set; }
        public double latitudNew { get; set; }
        public double longitud { get; set; }
        public double longitudNew { get; set; }
        public int selDolly { get; set; }
        public string telopera { get; set; }
        public int ClvViajTum { get; set; }
        public int seleuni { get; set; }
        public int Opera { get; set; }
        public string Ruta { get; set; }
        public int remolque1 { get; set; }
        public int remolque2 { get; set; }
        public int SelectOperacion { get; set; }
        public string clavesFalAndComen { get; set; }
        public string fallallantas { get; set; }
        public int cveEmp { get; set; }
        public int selcveEquipo { get; set; }
        public List<Error> Eror { get; set; }

    }
    public class TBCATTipoMtto
    {
        public int ClaveTipoMtto { get; set; }
        public string Descripcion { get; set; }
    }
    public class TBCATOrigenTicket
    {
        public int ClaveOrigenTicket { get; set; }
        public string Descripcion { get; set; }
        public string Alias { get; set; }
    }
    public class TBCAT_Dolly_S
    {
        public int ClaveDolly { get; set; }
        public string? Numero { get; set; }
        public int ClaveTipoOperacion { get; set; }
    }
    public class TBCATTipoClasificacion
    {
        public int ClaveTipoClasificacion { get; set; }
        public string? Descripcion { get; set; }
    }
    public class TBCATTipoTicket
    {
        public int ClaveTipoTicket { get; set; }
        public string? Descripcion { get; set; }
    }
    public class TBCATOperador
    {
        public int ClaveOperador { get; set; }
        public string? NumOP { get; set; }
        public string? Nombre { get; set; }
    }

    public class TBCATSeleccionEquipo
    {
        public int ClaveSeleccion { get; set; }
        public string? Descripcion { get; set; }
    }

    public class TBCATTipoApoyo
    {
        public int ClaveTipoApoyo { get; set; }
        public string? Descripcion { get; set; }
    }

    public class TBCATTipoCarga
    {
        public int ClaveTipoCarga { get; set; }
        public string? Descripcion { get; set; }
    }

    public class TBCATTipoEquipo
    {
        public int ClaveTipoEquipo { get; set; }
        public string? Descripcion { get; set; }
    }

    public class TBCATTipoFalla
    {
        public int ClaveTipoFalla { get; set; }
        public string? Descripcion { get; set; }
    }

    public class TBCATUnidade
    {
        public int ClaveUnidad_Motora { get; set; }
        public int Numero { get; set; }
        public string? Alias { get; set; }
        public int ClaveTipoOperacion { get; set; }
        public int ClaveTipoEquipo { get; set; }
        public string? TipoEquipo { get; set; }
        public string? TipoOperacion { get; set; }
    }
    public class UltimaPosicion
    {
        public string? UnitNum { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string? Position { get; set; }
        public DateTime SendTime { get; set; }
    }

    public class TBCATRemolque
    {
        public int ClaveUnidad_Arrastre { get; set; }
        public string? Numero { get; set; }
        public int ClaveTipoOperacion { get; set; }
    }
}
