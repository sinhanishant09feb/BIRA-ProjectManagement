using BIRA_Project_Management.Data;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BIRA_Project_Management.Service.DataBase.Implementation {
    public class IssueUnderProjectService : IIssueUnderProject<Issue> {
        private DataBaseContext dbContext;

        public IssueUnderProjectService(DataBaseContext dataBaseContext) {
            this.dbContext = dataBaseContext;
        }

        ///<summary>
        ///Deletes from Issue Table
        ///</summary>
        ///<param int = ProjectId and int = Issue Id></param>
        ///<returns>string = "message"</returns>
        public string Delete(int pId, int id) {
            var obj = dbContext.Issue.Where(i => i.Id == id && i.ProjectId == pId).SingleOrDefault();
            if (obj != null) {
                dbContext.Issue.Remove(obj);
                dbContext.SaveChanges();
                return BiraEnums.message.Issue_Removed.ToString();
            }
            return BiraEnums.message.Not_Found.ToString();
        }

        ///<summary>
        ///Reads from the Issue Table
        ///</summary>
        ///<param int = projectId></param>
        ///<returns>List<Issue> obj</returns>
        public List<Issue> GetAll(int pId) {
            List<Issue> list = new();
            var issues = dbContext.Issue
               .Where(p => p.ProjectId == pId);
            foreach (var issue in issues) {
                list.Add(new Issue(issue.Id, issue.Type, issue.Title, issue.Description,
                    issue.Reporter, issue.Status, issue.Assignee, issue.ProjectId));
            }
            return list;
        }

        ///<summary>
        ///Gets the record from Issue Table with id
        ///</summary>
        ///<param int = projectId and int = Issue Object Id></param>
        ///<returns>Issue obj</returns>
        public Issue GetOne(int pId, int id) {
            Issue result;
            var issue = dbContext.Issue.Where(i => i.Id == id && i.ProjectId == pId).SingleOrDefault();
            if (issue == null) {
                return new Issue();
            }
            result = new Issue(issue.Id, issue.Type, issue.Title, issue.Description, issue.Reporter,
               issue.Status, issue.Assignee, issue.ProjectId);
            return result;
        }

        ///<summary>
        ///Insert into Issue Table
        ///</summary>
        ///<param int = projectId and T = Issue Object></param>
        ///<returns>string = "message"</returns>
        public string Save(int pId, Issue t) {
            if (t == null)
                return BiraEnums.message.Bad_Request.ToString();
            Issue i = new(t.Type, t.Title, t.Description, t.Reporter, t.Assignee,
                t.Status, pId);
            dbContext.Issue.Add(i);
            dbContext.SaveChanges();
            return BiraEnums.message.Issue_Added_Succesfully.ToString();
        }

        ///<summary>
        ///Updates the Issue Table
        ///</summary>
        ///<param pId = projectId and T = Issue Object></param>
        ///<returns>string = "message"</returns>
        public string Update(int pId, Issue t) {
            var obj = dbContext.Issue.Where(p => p.Id == t.Id && p.ProjectId == pId).SingleOrDefault();
            if (obj != null) {
                obj.Type = t.Type;
                obj.Title = t.Title;
                obj.Description = t.Description;
                obj.Assignee = t.Assignee;
                obj.Status = t.Status;
                dbContext.SaveChanges();
                return BiraEnums.message.Issue_Updated_Succesfully.ToString();
            }
            return BiraEnums.message.Not_Found.ToString();
        }
    }
}
