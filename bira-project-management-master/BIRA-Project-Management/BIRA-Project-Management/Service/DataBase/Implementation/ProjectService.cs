using BIRA_Project_Management.Data;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BIRA_Project_Management.Service.DataBase.Implementation {
    public class ProjectService : IRepositoryService<Project> {
        private DataBaseContext dbContext;

        public ProjectService(DataBaseContext dataBaseContext) {
            this.dbContext = dataBaseContext;
        }

        ///<summary>
        ///Insert into Project Table
        ///</summary>
        ///<param T = Project Object></param>
        ///<returns>string = "message"</returns>
        public string Save(Project t) {
            if (t == null)
                return BiraEnums.message.Bad_Request.ToString();
            dbContext.Projects.Add(t);
            dbContext.SaveChanges();
            return BiraEnums.message.Project_Added_Succesfully.ToString();
        }

        ///<summary>
        ///Updates the Project Table
        ///</summary>
        ///<param T = Project Object></param>
        ///<returns>string = "message"</returns>
        public string Update(Project t) {
            var obj = dbContext.Projects.Where(p => p.Id == t.Id).SingleOrDefault();
            if (obj != null) {
                obj.Description = t.Description;
                obj.Creator = t.Creator;
                dbContext.SaveChanges();
                return BiraEnums.message.Project_Updated_Succesfully.ToString();
            }
            return BiraEnums.message.Not_Found.ToString();
        }

        ///<summary>
        ///Deletes from Project Table
        ///</summary>
        ///<param id = Project Object Id></param>
        ///<returns>string = "message"</returns>
        string IRepositoryService<Project>.Delete(int id) {
            var obj = dbContext.Projects.Where(p => p.Id == id).SingleOrDefault();
            if (obj != null) {
                dbContext.Projects.Remove(obj);
                dbContext.SaveChanges();
                return BiraEnums.message.Project_Removed.ToString();
            }
            return BiraEnums.message.Not_Found.ToString();
        }

        ///<summary>
        ///Reads from the Project Table
        ///</summary>
        ///<param></param>
        ///<returns>List<Project> obj</returns>
        List<Project> IRepositoryService<Project>.GetAll() {
            List<Project> list = new List<Project>();
            var projects = dbContext.Projects
               .Select(p => p);
            foreach(var project in projects) {
                list.Add(new Project(project.Id, project.Description, project.Creator));
            }
            return list;
         }

        ///<summary>
        ///Gets the record from Project Table with id
        ///</summary>
        ///<param int = Project Object Id></param>
        ///<returns>Project obj</returns>
        Project IRepositoryService<Project>.GetOne(int id) {
            Project result;
            var project = dbContext.Projects
               .Find(id);
            if (project == null) {
                return new Project();
            }
            result = new Project(project.Id, project.Description, project.Creator);
            return result;
        }   
    }
}
