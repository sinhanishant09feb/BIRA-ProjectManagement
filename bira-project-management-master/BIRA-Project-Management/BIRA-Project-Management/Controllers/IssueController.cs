using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BIRA_Project_Management.Controllers {
    [Route("api/issues")]
    [ApiController]
    public class IssueController : ControllerBase {
        private IRepositoryService<Issue> _issueService;
        private IIssueService _iservice;

        public IssueController(IRepositoryService<Issue> issueService, IIssueService issue) {
            this._issueService = issueService;
            this._iservice = issue;
        }

        ///<summary>
        ///Reads from issue table
        ///</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetIssues() {
            return Ok(_issueService.GetAll());
        }

        ///<summary>
        ///Finds issue with Id
        ///</summary>
        [HttpGet("id/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetIssueWithId(int Id) {
            var result = _issueService.GetOne(Id);
            if (result.Id == 0)
                return NotFound();
            return Ok(result);
        }

        ///<summary>
        ///Deletes issue with Id
        ///</summary>
        [HttpDelete("id/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RemoveIssue(int Id) {
            string msg = _issueService.Delete(Id);
            if (msg.Equals(BiraEnums.message.Issue_Removed.ToString()))
                return NoContent();
            return NotFound();
        }

        ///<summary>
        ///Posts an issue
        ///</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddIssue([FromBody] Issue issue) {
            var view = _issueService.Save(issue);
            if (view.Equals(BiraEnums.message.Issue_Added_Succesfully.ToString()))
                return new ObjectResult(view) { StatusCode = StatusCodes.Status201Created };
            return BadRequest();
        }

        ///<summary>
        ///Updates an issue
        ///</summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateIssue([FromBody] Issue issue) {
            string msg = _issueService.Update(issue);
            if (msg.Equals(BiraEnums.message.Issue_Updated_Succesfully.ToString()))
                return new ObjectResult(msg) { StatusCode = StatusCodes.Status202Accepted };
            return BadRequest();
        }

        ///<summary>
        ///Updates status of an issue
        ///</summary>
        [HttpPut("/status/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateStatus(int id) {
            string msg = _iservice.UpdateStatus(id);
            if (msg.Equals(BiraEnums.message.Status_Updated_Succesfully.ToString()))
                return new ObjectResult(msg) { StatusCode = StatusCodes.Status202Accepted };
            return NotFound();
        }

        ///<summary>
        ///Updates asignee of an issue
        ///</summary>
        [BasicAuthorization]
        [HttpPut("/assignee/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult updateAsignee(int id) {
            string msg = _iservice.UpdateAssignee(id);
            if (msg.Equals(BiraEnums.message.Issue_Updated_Succesfully.ToString()))
                return new ObjectResult(msg) { StatusCode = StatusCodes.Status202Accepted };
            else if (msg.Equals(BiraEnums.message.Not_Found.ToString()))
                return NotFound();
            return BadRequest();
        }

        ///<summary>
        ///Reads from Issue Table
        ///</summary>
        [HttpGet("/title/description")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetByTitleAndDescription([FromQuery] string title = "",
            [FromQuery] string description = "") {
            var issue = _iservice.SearchByTitleAndDesc(title, description);
            if (issue.Id != 0)
                return Ok(issue);
            return NotFound();
        }
    }
}