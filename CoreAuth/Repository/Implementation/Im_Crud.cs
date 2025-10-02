using CoreAuth.Models.Table;
using CoreAuth.Repository.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreAuth.Repository.Implementation
{
    public class Im_Crud(IDapper con) : Icrud
    {
        public bool Delete(string tableName, string colName, string id)
        {
            try
            {
                int.TryParse(id, out int parsedId);
                
                using (var connection = new SqlConnection(con.Dappercon()))
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

                using (var connection = new SqlConnection(con.Dappercon()))
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
        public List<dynamic> GetTableData(string TableName)
{
    try
    {
        using (var connection = new SqlConnection(con.Dappercon()))
        {
            string sql = $@"SELECT * FROM [{TableName}]";
            var data = connection.Query(sql).ToList();
            return data;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return new List<dynamic>();
    }
}


public List<SelectListItem> GetDDTableData(string tableName, string idCol, string valueCol)
{
    using (var connection = new SqlConnection(con.Dappercon()))
    {
      
        string sql =
            $"SELECT [{idCol}] AS Id, [{valueCol}] AS Val FROM [{tableName}]";

        var rows = connection.Query(sql)
                             .Select(r => new {
                                 Id = (object)r.Id,   
                                 Val = (string)r.Val  
                             })
                             .ToList();

        var selectList = rows.Select(r => new SelectListItem
        {
            Value = r.Id.ToString(),
            Text = r.Val
        }).ToList();

        return selectList;
    }
}
    }
}
