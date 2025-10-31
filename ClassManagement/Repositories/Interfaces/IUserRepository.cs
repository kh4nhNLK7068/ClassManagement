using ClassManagement.Models.Entities;
using System.Collections.Generic;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    User GetUser(string username, string password);
    void Add(User user);
    void Update(User user);
    void Delete(int id);
}
