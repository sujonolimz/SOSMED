using Microsoft.Data.SqlClient;
using System.Data;

namespace SOSMED_API.Helpers
{
    public class SqlServerConnector
    {
        private readonly string _connectionString;

        public SqlServerConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteNonQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var dataTable = new DataTable();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    return dataTable;
                }
            }
        }
    }
}
