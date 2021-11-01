
using BIRA_Project_Management;
using BIRA_Project_Management.Controllers;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace BIRA_Test {
    public class IssueTest {
        IRepositoryService<Issue> iRepService;
        IIssueService iIssService;
        IssueController controller;

        public IssueTest() {
            iRepService = A.Fake<IRepositoryService<Issue>>();
            iIssService = A.Fake<IIssueService>();
            controller = new IssueController(iRepService, iIssService);
        }

        [Fact]
        public void TestGetAll() {
            //Arrange
            int count = 5;
            var fakeIssues = A.CollectionOfDummy<Issue>(5) as List<Issue>;
            A.CallTo(() => iRepService.GetAll()).Returns(fakeIssues);
            //Act
            var actionResult = controller.GetIssues();
            //Assert
            var result = actionResult as OkObjectResult;
            var returnIssues = result.Value as List<Issue>;
            Assert.Equal(count, returnIssues.Count);
        }

        [Fact]
        public void TestGetIssueWithId() {
            //Arrange
            int id = 1;
            var fakeIssue = A.Dummy<Issue>();
            A.CallTo(() => iRepService.GetOne(id)).Returns(fakeIssue);
            //Act
            var actionResult = controller.GetIssueWithId(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestRemoveIssue() {
            //Arrange
            int id = 1;
            A.CallTo(() => iRepService.Delete(id)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.RemoveIssue(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateIssue() {
            //Arrange
            Issue issue = new Issue();
            A.CallTo(() => iRepService.Update(issue)).Returns(BiraEnums.message.Bad_Request.ToString());
            //Act
            var actionResult = controller.UpdateIssue(issue);
            //Assert
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestAddIssue() {
            //Arrange
            Issue issue = new Issue();
            A.CallTo(() => iRepService.Save(issue)).Returns(BiraEnums.message.Bad_Request.ToString());
            //Act
            var actionResult = controller.AddIssue(issue);
            //Assert
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateIssueStatus() {
            //Arrange
            int id = 1;
            A.CallTo(()=> iIssService.UpdateStatus(id)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.updateStatus(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateIssueAsignee() {
            //Arrange
            int id = 1;
            A.CallTo(() => iIssService.UpdateAssignee(id)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.updateAsignee(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetByTitleAndDescription() {
            //Arrange
            string title = "";
            string description = "";
            Issue issue = new Issue();
            A.CallTo(()=> iIssService.SearchByTitleAndDesc(title, description)).Returns(issue);
            //Act
            var actionResult = controller.GetByTitleAndDescription(title, description);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }
    }   
}
