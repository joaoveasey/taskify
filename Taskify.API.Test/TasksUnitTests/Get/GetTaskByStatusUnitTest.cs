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
    public class GetTaskByStatusUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public GetTaskByStatusUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task GetTaskByStatus_ReturnsAllItems_Done()
        {
            // act
            var result = await _controller.FilterTasksByStatus(true);

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<Models.Tasks>>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetTaskByStatus_ReturnsAllItems_NotDone()
        {
            // act
            var result = await _controller.FilterTasksByStatus(false);

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<Models.Tasks>>()
                .And.NotBeNull();
        }
    }
}
