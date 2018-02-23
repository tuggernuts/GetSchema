using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ClassLibrary
{
    public class SomeClass
    {
        //todo: set connection string as necessary
        private const string CONN_STRING = "my string";
        public DataTable GetSchema()
        {
            try
            {
                using (var connection = new SqlConnection(CONN_STRING))
                {


                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        // these are sample array elements, change accordingly
                        var table = connection.GetSchema("columns",
                            new[] {"catalog", "dbo", "table", null});
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
