using System.Data;
using System.Configuration;
using Npgsql;

namespace ref_and_out
{
    class Test
    {
        static void Main(string[] args)
        {
            string sql = "SELECT * FROM mahasiswa";

            NpgsqlParameter[] parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("umur", 20),
                new NpgsqlParameter("nim", "212410103044")
            };

            DataSet ds = new DataSet();
            bool isSucces = SqlHelper.ExecuteDataSet(ref ds, sql, parameters);
            if (isSucces)
            {
                DataTableReader reader = ds.CreateDataReader();

                while (reader.Read())
                {
                    Console.Write(reader.GetString(2) + " ");
                    Console.WriteLine(reader.GetInt32(3));
                }
            }
            
        }
    }

    static class SqlHelper
    {
        internal static bool ExecuteDataSet(ref DataSet ds, string sql, NpgsqlParameter[] parameters)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

                connection.Open();

                foreach (NpgsqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                new NpgsqlDataAdapter(cmd).Fill(ds);

                connection.Close();

                return true;
            } 
            catch (Exception e)
            {
                Console.WriteLine("Terjadi Kesalahan ");
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }

    
}