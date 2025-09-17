using CoreAuth.Models.Table;
using CoreAuth.Repository.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public List<dynamic> ShowIndivisualRow(string TableName, string ColName, string Id)
        {
            int.TryParse(Id, out int parsedId);
            try
            {

                using (var connection = new SqlConnection(con))
                {
                    string sql = $@"
                SELECT * 
                FROM [{TableName}] 
                WHERE [{ColName}] = @Id";

                    var data = connection.Query(sql, new { Id = Id }).ToList(); 
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<dynamic>();
            }
        }
    }
}
