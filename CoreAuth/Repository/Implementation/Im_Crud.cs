using CoreAuth.Models.Table;
using CoreAuth.Repository.Interface;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CoreAuth.Repository.Implementation
{
    public class Im_Crud(IConfiguration con) : Icrud
    {
        readonly private string con = con.GetConnectionString("DefaultConnection");
        public bool Delete(string tableName, string colName, string id)
        {
            try
            {
                int.TryParse(id, out int parsedId);
                
                using (var connection = new SqlConnection(con))
                {
                    string sql = $@"DELETE FROM [{tableName}] WHERE [{colName}] = @Id";

                    int rows = connection.Execute(sql, new { Id = id });

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
