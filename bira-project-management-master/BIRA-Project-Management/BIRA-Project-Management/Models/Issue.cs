using System.ComponentModel.DataAnnotations;

namespace BIRA_Project_Management.Models {
    public class Issue {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public Issue(string type, string title, string description, string reporter, string assignee,
            string status, int projectId) {
            Type = type;
            Title = title;
            Description = description;
            Reporter = reporter;
            Assignee = assignee;
            Status = status;
            ProjectId = projectId;
        }
        
        public Issue(int id, string type, string title, string description, string reporter, 
            string status, string assignee, int projectId) {
            Id = id;
            Type = type;
            Title = title;
            Description = description;
            Reporter = reporter;
            Status = status;
            Assignee = assignee;
            ProjectId = projectId;
        }

        public Issue() { }
    }
}
