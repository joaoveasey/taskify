using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.API.Controllers;

namespace Taskify.API.Test.UnitTests.TasksTest.Get
{
    public class GetAllTasksUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public GetAllTasksUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task GetAllTasks_WhenCalled_ReturnsAllItems()
        {
            // act
            var result = await _controller.GetAllTasks();

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<Taskify.API.Models.Tasks>>()
                .And.NotBeNull();
        }
        
    }
}
