﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Taskify.API.Interfaces;
using Taskify.API.Models;

namespace Taskify.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task")]
    [EnableRateLimiting("fixedwindow")]
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

            if (tasks is null)
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
        public async Task<ActionResult<Tasks>> RemoveTask(int id)
        {
            var task = await _unitOfWork.TasksRepository.GetByIdAsync(id);

            if (task is null)
                return BadRequest("Erro ao remover tarefa.");

            await _unitOfWork.TasksRepository.RemoveAsync(task);

            return Ok(task);
        }

        [HttpGet("filter/due-date")]
        [SwaggerOperation(Summary = "Filtra tarefas por data de vencimento.", 
            Description = "Filtra tarefas baseadas na data de vencimento fornecida no formato yyyy-MM-dd.")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByDate(DateTime date)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByDateAsync(date);

            if (tasks is null)
                return NotFound("Nenhuma tarefa encontrada.");

            return Ok(tasks);
        }

        [HttpGet("filter/priority")]
        [SwaggerOperation(Summary = "Filtra tarefas por prioridade.",
            Description = "Filtra tarefas com base na prioridade fornecida: \"Baixa\", \"Média\" ou \"Alta\".")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByPriority(string priority)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByPriorityAsync(priority);

            if (tasks is null || priority is null || priority != "Baixa" && priority != "Média" && priority != "Alta")
                return NotFound("Nenhuma tarefa encontrada.");

            return Ok(tasks);
        }

        [HttpGet("filter/status")]
        [SwaggerOperation(Summary = "Filtra tarefas por status.",
            Description = "Filtra tarefas com base no status: Concluída (true) ou Não Concluída (false).")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByStatus(bool status)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByStatusAsync(status);

            if (tasks is null)
                return NotFound("Nenhuma tarefa encontrada.");

            return Ok(tasks);
        }
    }
}
