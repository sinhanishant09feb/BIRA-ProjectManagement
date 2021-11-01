using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BIRA_Project_Management.Controllers {
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase {
        private IRepositoryService<Project> _projectService;
        private IIssueUnderProject<Issue> _issueService;

        public ProjectController(IRepositoryService<Project> projectService,
            IIssueUnderProject<Issue> issueUnderProject) {
            this._projectService = projectService;
            this._issueService = issueUnderProject;
        }

        ///<summary>
        ///Reads from Project Table
        ///</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProjects() {      
            return Ok(_projectService.GetAll());
        }

        ///<summary>
        ///Posts into Project Table
        ///</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProject([FromBody] Project project) {
            string view = _projectService.Save(project);
            if(view.Equals(BiraEnums.message.Project_Added_Succesfully.ToString()))
                return new ObjectResult(view) { StatusCode = StatusCodes.Status201Created };
            return BadRequest();
        }

        ///<summary>
        ///Updates a Project
        ///</summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProject([FromBody] Project project) {
            string msg = _projectService.Update(project);
            if (msg.Equals(BiraEnums.message.Project_Updated_Succesfully.ToString()))
                return new ObjectResult(msg) { StatusCode = StatusCodes.Status202Accepted };
            return BadRequest();
         }

        ///<summary>
        ///Finds a project with Id
        ///</summary>
        [HttpGet("id/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProjectWithId(int Id) {
            var result = _projectService.GetOne(Id);
            if (result.Id == 0)
                return NotFound();
            return Ok(result);
        }

        ///<summary>
        ///Deletes a project with Id
        ///</summary>
        [HttpDelete("id/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RemoveProject(int Id) {
            string msg = _projectService.Delete(Id);
            if (msg.Equals(BiraEnums.message.Project_Removed.ToString()))
                return NoContent();
            return NotFound();       
        }

        ///<summary>
        ///Get issues under project
        ///</summary>
        [HttpGet("id/{Id}/Issues")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetIssuesByProjectId(int Id) {
            List<Issue> list = _issueService.GetAll(Id);
            if (!list.Any())
                return NotFound();
            return Ok(list);
        }

        ///<summary>
        ///Adds issue under project
        ///</summary>
        [HttpPost("id/{Id}/Issues")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddIssueUnderProject(int Id, [FromBody] Issue issue) {
            var view = _issueService.Save(Id, issue);
            if(view.Equals(BiraEnums.message.Issue_Added_Succesfully.ToString()))
                return new ObjectResult(view) { StatusCode = StatusCodes.Status201Created };
            return BadRequest();
        }

        ///<summary>
        ///Updates issue under project
        ///</summary>
        [HttpPut("id/{Id}/Issues")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateIssueUnderProject(int Id, [FromBody] Issue issue) {
            string msg = _issueService.Update(Id, issue);
            if (msg.Equals(BiraEnums.message.Issue_Updated_Succesfully.ToString()))
                return new ObjectResult(msg) { StatusCode = StatusCodes.Status202Accepted };
            return NotFound();
        }

        ///<summary>
        ///Gets issue with Id under project
        ///</summary>
        [HttpGet("id/{Id}/Issues/{issueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetIssueWithIdUnderProject(int Id, int issueId) {
            var result = _issueService.GetOne(Id, issueId);
            if (result.Id == 0)
                return NotFound();
            return Ok(result);
        }

        ///<summary>
        ///Deletes an issue under project
        ///</summary>
        [HttpDelete("id/{Id}/Issues/{issueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteIssueUnderProject(int Id, int issueId) {
            string msg = _issueService.Delete(Id, issueId);
            if (msg.Equals(BiraEnums.message.Issue_Removed.ToString()))
                return NoContent();
            return NotFound();
        }  
    }
}
