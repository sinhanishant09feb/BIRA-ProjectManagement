using System.Collections.Generic;

namespace BIRA_Project_Management.Service.DataBase.Interface {
    public interface IRepositoryService<T> {
        List<T> GetAll();
        T GetOne(int id);
        string Save(T t);
        string Update(T t);
        string Delete(int id);
    }
}
