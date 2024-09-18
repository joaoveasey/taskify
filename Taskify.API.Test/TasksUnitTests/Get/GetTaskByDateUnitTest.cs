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
    public class GetTaskByDateUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public GetTaskByDateUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task GetTaskByDate_ReturnsAllItems()
        {
            // act
            var result = await _controller.FilterTasksByDate(DateTime.Now);

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<Models.Tasks>>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetTaskByDate_ReturnsNotFound()
        {
            // act
            var result = await _controller.FilterTasksByDate(DateTime.MinValue);

            // assert
            result.Result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be(404);
        }
    }
}
