using System.Collections.Generic;

namespace BIRA_Project_Management.Service.DataBase.Interface {
    public interface IIssueUnderProject<T> {
        List<T> GetAll(int pId);
        T GetOne(int pId, int id);
        string Save(int pId, T t);
        string Update(int pId, T t);
        string Delete(int pId, int id);
    }
}
