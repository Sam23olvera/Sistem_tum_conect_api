using System.Data;
using System.Data.SqlClient;

namespace ConectDB.DB
{
    public class Conexion : IDisposable
    {
        private string _conexionString;
        private SqlConnection _conexion;

        public Conexion() 
        {
            var bul = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _conexionString = bul.GetConnectionString("ConDB");
            _conexion = new SqlConnection(_conexionString);
        }
        public void Abrir() 
        {
            if (_conexion.State == ConnectionState.Closed) 
            {
                _conexion.Open();
            }
        }

        public void Close()
        {
            if (_conexion.State == ConnectionState.Open) 
            {
                _conexion.Close();
            }
        }

        public void Dispose()
        {
            Close();
            _conexion.Dispose();
        }

        public DataSet Ejecutarquery(string query, bool esStoredProc) 
        {
            DataSet dat = new DataSet();
            try
            {
                using (var cmd = new SqlCommand(query,_conexion)) 
                {
                    if (esStoredProc) 
                    { 
                            cmd.CommandType = CommandType.StoredProcedure;
                    }

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dat);
                    }
                }
                
            }
            catch (Exception ex) 
            {
                throw new Exception($"Excepcion { ex.Message }",ex);
            }
            return dat;
        }
    }
}
