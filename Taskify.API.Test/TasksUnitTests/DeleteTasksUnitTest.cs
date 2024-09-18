using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.API.Controllers;

namespace Taskify.API.Test.UnitTests
{
    public class DeleteTasksUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public DeleteTasksUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task DeleteTask_ReturnsOk()
        {
            // act
            var result = await _controller.RemoveTask(1);

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<Models.Tasks>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task DeleteTask_ReturnsNotFound()
        {
            // act
            var result = await _controller.RemoveTask(0);

            // assert
            result.Result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be(404);
        }
    }
}
