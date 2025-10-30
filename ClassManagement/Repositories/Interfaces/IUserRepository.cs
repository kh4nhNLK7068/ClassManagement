using ClassManagement.Models.Entities;
using System.Collections.Generic;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(int id);
}
