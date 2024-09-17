using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.API.Controllers;

namespace Taskify.API.Test.UnitTests.Tasks
{
    public class PostTasksUnitTest : IClassFixture<TasksUnitTestController>
    {
        private readonly TaskController _controller;

        public PostTasksUnitTest(TasksUnitTestController controller)
        {
            _controller = new TaskController(controller.repository);
        }

        [Fact]
        public async Task PostTask_ReturnsOk()
        {
            // act
            var result = await _controller.AddTask(new Taskify.API.Models.Tasks 
            { Titulo = "Task 1", Descricao = "Task 1", DataVencimento = DateTime.Now, Concluida = false });

            // assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<Taskify.API.Models.Tasks>()
                .And.NotBeNull();
        }
    }
}
