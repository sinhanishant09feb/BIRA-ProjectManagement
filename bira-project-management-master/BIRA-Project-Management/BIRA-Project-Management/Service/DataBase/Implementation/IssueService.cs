using BIRA_Project_Management.Data;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BIRA_Project_Management.Service.DataBase.Implementation {
    public class IssueService : IRepositoryService<Issue>, IIssueService {
        private DataBaseContext dbContext;
        private Dictionary<string, int> statusDictionaty;

        public IssueService(DataBaseContext dataBaseContext) {
            this.dbContext = dataBaseContext;
            statusDictionaty = new Dictionary<string, int> {
                { "Open", 1 }, { "In Progress", 2 }, { "In Review", 3 },
                { "Code Complete", 4}, { "QA Testing", 5 }, { "Done", 6 }
            };
        }

        ///<summary>
        ///Deletes from Issue Table
        ///</summary>
        ///<param id = Issue Object Id></param>
        ///<returns>string = "message"</returns>
        public string Delete(int id) {
#pragma warning restore CS1570 // XML comment has badly formed XML
            var obj = dbContext.Issue.Where(i => i.Id == id).SingleOrDefault();
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
        ///<param></param>
        ///<returns>List<Issue> obj</returns>
        public List<Issue> GetAll() {
#pragma warning restore CS1570 // XML comment has badly formed XML
            List<Issue> list = new List<Issue>();
            var issues = dbContext.Issue
               .Select(p => p);
            foreach (var issue in issues) {
                list.Add(new Issue(issue.Id, issue.Type, issue.Title,issue.Description,
                    issue.Reporter, issue.Status ,issue.Assignee, issue.ProjectId));
            }
            return list;
        }

        ///<summary>
        ///Gets the record from Issue Table with id
        ///</summary>
        ///<param int = Issue Object Id></param>
        ///<returns>Issue obj</returns>
        public Issue GetOne(int id) {
            Issue result;
            var issue = dbContext.Issue
               .Find(id);
            if (issue == null) {
                return new Issue();
            }
            result = new Issue(issue.Id,issue.Type, issue.Title,issue.Description, issue.Reporter,
               issue.Status, issue.Assignee, issue.ProjectId);
            return result;
        }

        ///<summary>
        ///Insert into Issue Table
        ///</summary>
        ///<param T = Issue Object></param>
        ///<returns>string = "message"</returns>
        public string Save(Issue t) {
            if (t == null)
                return BiraEnums.message.Bad_Request.ToString();
            dbContext.Issue.Add(t);
            dbContext.SaveChanges();
            return BiraEnums.message.Issue_Added_Succesfully.ToString();
        }

        ///<summary>
        ///Search By Title and Description
        ///</summary>
        ///<param string = title and string = description></param>
        ///<returns>Issue object</returns>
        public Issue SearchByTitleAndDesc(string title, string description) {
            Issue issue;
            if (title.Equals("")) {
                if (description.Equals(""))
                    issue = new Issue();
                else
                    issue = dbContext.Issue.Where(i => i.Description == description)
                        .SingleOrDefault();
            } else {
                if (description.Equals(""))
                    issue = dbContext.Issue.Where(i=> i.Title == title)
                        .SingleOrDefault();
                else
                    issue = dbContext.Issue.Where(i => i.Description == description
                        && i.Title == title).SingleOrDefault();
            }
            return issue;
        }

        ///<summary>
        ///Updates the Issue Table
        ///</summary>
        ///<param T = Issue Object></param>
        ///<returns>string = "message"</returns>
        public string Update(Issue t) {
            var obj = dbContext.Issue.Where(p => p.Id == t.Id).SingleOrDefault();
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

        ///<summary>
        ///Updates the Asignee in Issue Table
        ///</summary>
        ///<param int = isssueId and string = assigneeName></param>
        ///<returns>string = "message"</returns>
        public string UpdateAssignee(int id) {
            var issue = dbContext.Issue.Where(i => i.Id == id).SingleOrDefault();
            if (issue == null)
                return BiraEnums.message.Not_Found.ToString();
            issue.Assignee = Authentication.BasicAuthenticationHandler.user;
            dbContext.SaveChanges();
            return BiraEnums.message.Issue_Updated_Succesfully.ToString();
        }

        ///<summary>
        ///Updates the Status in Issue Table
        ///</summary>
        ///<param int = isssueId></param>
        ///<returns>string = "message"</returns>
        public string UpdateStatus(int id) {
            var obj = dbContext.Issue.Where(i => i.Id == id).SingleOrDefault();
            if (obj != null) {
                int statusValue = statusDictionaty[obj.Status];
                if (statusValue < 6) {
                    obj.Status = statusDictionaty.FirstOrDefault(s => s.Value == (statusValue + 1)).Key;
                    dbContext.SaveChanges();
                }
                return BiraEnums.message.Status_Updated_Succesfully.ToString();
            }
            return BiraEnums.message.Not_Found.ToString();
        }
    }
}
