using Dapper;
using InfraEstrutura.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace InfraEstrutura.Repository
{
    public class TodoItemRepository
    {

        private string ConnectionString = "Server=tcp:listadeamigo.database.windows.net,1433;Initial Catalog=listadeamigo_;Persist Security Info=False;User ID=santireis14;Password=7244511rrs$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public List<TodoItem> GetAll()
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM Tbl_Todo_Item";
                return connection.Query<TodoItem>(sql).ToList();
            }
        }

        public TodoItem GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM Tbl_Todo_Item where id = @id ";
                return connection.QueryFirstOrDefault<TodoItem>(sql, new { id = id});
            }
        }

        public void Save(TodoItem item)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = "INSERT INTO Tbl_Todo_Item (AssignedFor, Status, Name, Description) Values (@AssignedFor,@Status,@Name,@Description)";
                connection.Execute(sql,item);
            }
        }
        
        public void Delete(TodoItem item)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = "DELETE FROM Tbl_Todo_Item where id = @id ";
                connection.Execute(sql, item);
            }
        }

    }
}
