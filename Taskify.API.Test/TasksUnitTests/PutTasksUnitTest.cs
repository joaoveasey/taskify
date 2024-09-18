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
    public class PutTasksUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public PutTasksUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task PutTask_ReturnsOk()
        {
            // act
            var result = await _controller.UpdateTask(new Models.Tasks
            { Id = 1, Titulo = "Task 1", Descricao = "Task 1", DataVencimento = DateTime.Now, Concluida = false });

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<Models.Tasks>()
                .And.NotBeNull();
        }
    }
}
