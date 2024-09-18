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
    public class GetTaskByIdUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public GetTaskByIdUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task GetTaskById_WhenCalled_ReturnsItem()
        {
            // act
            var result = await _controller.GetTaskById(1);

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<Models.Tasks>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetTaskById_WhenCalled_ReturnsNotFound()
        {
            // act
            var result = await _controller.GetTaskById(999999);

            // assert
            result.Result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be(404);
        }
    }
}
