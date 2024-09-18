using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.API.Controllers;

namespace Taskify.API.Test.UnitTests.Get
{
    public class GetTaskByPriorityUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public GetTaskByPriorityUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task GetTaskByPriority_ReturnsAllItems()
        {
            // act
            var result = await _controller.FilterTasksByPriority("Média");

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<Models.Tasks>>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetTaskByPriority_ReturnsNotFound()
        {
            // act
            var result = await _controller.FilterTasksByPriority(null);

            // assert
            result.Result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be(404);
        }
    }
}
