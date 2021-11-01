using BIRA_Project_Management.Models;

namespace BIRA_Project_Management.Service.DataBase.Interface {
    public interface IIssueService {
        string UpdateAssignee(int id);
        string UpdateStatus(int id);
        Issue SearchByTitleAndDesc(string title, string description);
    }
}
