using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ClassLibrary
{
    public class SomeClass
    {
        private const string CONN_STRING = "Data Source=tcp:otto.4broadcast.net;Initial Catalog=vodka2_dev;Integrated Security=True;Persist Security Info=True;TrustServerCertificate=True";
        public DataTable GetSchema()
        {
            try
            {
                using (var connection = new SqlConnection(CONN_STRING))
                {


                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var table = connection.GetSchema("columns",
                            new[] {"vodka2_dev", "dbo", "Companies", null});
                        return table;
                    }

                }

                return null;
            }
            catch (SqlException exception)
            {
                Debug.WriteLine(exception.Message);
                return null;

            }
            catch (InvalidCastException exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }

            catch (ArgumentNullException exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            

        }

        public List<string> GetList()
        {
            var query = "Select Name from Companies;";

            var result = new List<string>();
            try
            {
                using (var connection = new SqlConnection(CONN_STRING))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = query;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    result.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
