using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BIRA_Project_Management.Models {
    public class Project {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public ICollection<Issue> issues { get; set; }

        public Project() { }

        public  Project(string desc, string creat) {
            this.Description = desc;
            this.Creator = creat;
        }

        public Project(int id, string desc, string creat) {
            this.Id = id;
            this.Description = desc;
            this.Creator = creat;
        }
    }
}
