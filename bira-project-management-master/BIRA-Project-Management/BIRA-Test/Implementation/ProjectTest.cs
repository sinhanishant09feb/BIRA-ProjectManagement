
using BIRA_Project_Management;
using BIRA_Project_Management.Controllers;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Service.DataBase.Interface;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace BIRA_Test {
    public class ProjectTest {
        IRepositoryService<Project> iRepService;
        IIssueUnderProject<Issue> iIssService;
        ProjectController controller;

        public ProjectTest() {
            iRepService = A.Fake<IRepositoryService<Project>>();
            iIssService = A.Fake<IIssueUnderProject<Issue>>();
            controller = new ProjectController(iRepService, iIssService);
        }

        [Fact]
        public void TestGetAll() {
            //Arrange
            int count = 5;
            var fakeProjects = A.CollectionOfDummy<Project>(count) as List<Project>;
            A.CallTo(() => iRepService.GetAll()).Returns(fakeProjects);
            //Ac
            var actionResult = controller.GetProjects();
            //Assert
            var result = actionResult as OkObjectResult;
            var returnProjects = result.Value as List<Project>;
            Assert.Equal(count, returnProjects.Count);
        }

        [Fact]
        public void TestGetProjectWithId() {
            //Arrange
            int id = 1;
            var fakeIssue = A.Dummy<Project>();
            A.CallTo(() => iRepService.GetOne(id)).Returns(fakeIssue);
            //Act
            var actionResult = controller.GetProjectWithId(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestRemoveProject() {
            //Arrange
            int id = 1;
            A.CallTo(() => iRepService.Delete(id)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.RemoveProject(id);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateProject() {
            //Arrange
            Project project = new Project();
            A.CallTo(() => iRepService.Update(project)).Returns(BiraEnums.message.Bad_Request.ToString());
            //Act
            var actionResult = controller.UpdateProject(project);
            //Assert
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestAddProject() {
            //Arrange
            Project project = new Project();
            A.CallTo(() => iRepService.Save(project)).Returns(BiraEnums.message.Bad_Request.ToString());
            //Act
            var actionResult = controller.AddProject(project);
            //Assert
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetIssuesByProjectId() {
            //Arrange
            int count = 5;
            int id = 1;
            var fakeIssues = A.CollectionOfDummy<Issue>(count) as List<Issue>;
            A.CallTo(() => iIssService.GetAll(id)).Returns(fakeIssues);
            //Ac
            var actionResult = controller.GetIssuesByProjectId(id);
            //Assert
            var result = actionResult as OkObjectResult;
            var returnIssues = result.Value as List<Issue>;
            Assert.Equal(count, returnIssues.Count);
        }

        [Fact]
        public void TestGetIssueWithIdUnderProject() {
            //Arrange
            int id = 1;
            int issueId = 1;
            var fakeIssue = A.Dummy<Issue>();
            A.CallTo(() => iIssService.GetOne(id, issueId)).Returns(fakeIssue);
            //Act
            var actionResult = controller.GetIssueWithIdUnderProject(id, issueId);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestRemoveIssueUnderProject() {
            //Arrange
            int id = 1;
            int issueId = 1;
            A.CallTo(() => iIssService.Delete(id, issueId)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.DeleteIssueUnderProject(id, issueId);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateIssueUnderProject() {
            //Arrange
            int id = 1;
            Issue issue = new Issue();
            A.CallTo(() => iIssService.Update(id, issue)).Returns(BiraEnums.message.Not_Found.ToString());
            //Act
            var actionResult = controller.UpdateIssueUnderProject(id, issue);
            //Assert
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestIssueUnderProject() {
            //Arrange
            int id = 1;
            Issue issue = new Issue();
            A.CallTo(() => iIssService.Save(id, issue)).Returns(BiraEnums.message.Bad_Request.ToString());
            //Act
            var actionResult = controller.AddIssueUnderProject(id,issue);
            //Assert
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }
    }
}
