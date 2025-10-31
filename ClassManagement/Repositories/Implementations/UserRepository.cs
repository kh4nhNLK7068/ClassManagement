using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using ClassManagement.Models.Entities;
using System.Linq;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    private SqlConnection GetConnection() => new SqlConnection(_connectionString);

    public IEnumerable<User> GetAll()
    {
        using (var conn = GetConnection())
        {
            return conn.Query<User>("SELECT * FROM [User]");
        }
    }

    public User GetById(int id)
    {
        using (var conn = GetConnection())
        {
            return conn.QueryFirstOrDefault<User>("SELECT * FROM [User] WHERE Id = @Id", new { Id = id });
        }
    }

    public User GetUser(string username, string password)
    {
        using (var conn = GetConnection())
        {
            return conn.QueryFirstOrDefault<User>(
                "SELECT * FROM [User] WHERE Username = @Username AND Password = @Password",
                new { Username = username, Password = password }
            );
        }
    }

    public void Add(User user)
    {
        using (var conn = GetConnection())
        {
            string sql = "INSERT INTO [User](Username, Password) VALUES(@Username, @Password)";
            conn.Execute(sql, user);
        }
    }

    public void Update(User user)
    {
        using (var conn = GetConnection())
        {
            string sql = "UPDATE [User] SET Username=@Username, Password=@Password WHERE Id=@Id";
            conn.Execute(sql, user);
        }
    }

    public void Delete(int id)
    {
        using (var conn = GetConnection())
        {
            conn.Execute("DELETE FROM [User] WHERE Id=@Id", new { Id = id });
        }
    }

    IEnumerable<User> IUserRepository.GetAll()
    {
        throw new System.NotImplementedException();
    }

    User IUserRepository.GetById(int id)
    {
        throw new System.NotImplementedException();
    }
}
