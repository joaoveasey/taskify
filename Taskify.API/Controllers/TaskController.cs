﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taskify.API.Interfaces;
using Taskify.API.Models;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as tarefas.")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasks()
        {
            var tasks = await _unitOfWork.TasksRepository.GetAllAsync();

            if (tasks is not null)
                return NotFound("Nenhuma tarefa.");

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna uma tarefa pelo ID.")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)
        {
            var task = await _unitOfWork.TasksRepository.GetByIdAsync(id);

            if (task is null)
                return NotFound("Tarefa não encontrada.");

            return Ok(task);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova tarefa.")]
        public async Task<ActionResult<Tasks>> AddTask(Tasks task)
        {
            var newTask = await _unitOfWork.TasksRepository.AddAsync(task);

            if (newTask is null)
                return BadRequest("Erro ao adicionar tarefa.");

            return Ok(newTask);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza uma tarefa existente.")]
        public async Task<ActionResult<Tasks>> UpdateTask(Tasks task)
        {
            var updatedTask = await _unitOfWork.TasksRepository.UpdateAsync(task);

            if (updatedTask is null)
                return BadRequest("Erro ao atualizar tarefa.");

            return Ok(updatedTask);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Remove uma tarefa existente.")]
        public async Task<ActionResult<Tasks>> RemoveTask(Tasks task)
        {
            var removedTask = await _unitOfWork.TasksRepository.RemoveAsync(task);

            if (removedTask is null)
                return BadRequest("Erro ao remover tarefa.");

            return Ok(removedTask);
        }
    }
}
