using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TestTodoService.Sdk.Commands;
using TestTodoService.Sdk;
using TestTodoService.Sdk.Dtos;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly IBus bus;
        private readonly ITestTodoService testTodoService;

        public TodosController(
            IBus bus,
            ITestTodoService testTodoService)
        {
            this.bus = bus;
            this.testTodoService = testTodoService;
        }

        /// <summary>
        /// Get single todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoAsync([FromRoute] Guid id)
        {
            var user = await testTodoService.GetTodoAsync(id);

            return Ok(user);
        }

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            var user = await testTodoService.GetAllTodosAsync();

            return Ok(user);
        }

        /// <summary>
        /// Add todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>CommandId</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> AddTodoAsync([FromBody] Todo todo)
        {
            var commandId = await bus.SendCommandAsync<AddTodo>(new
            {
                Id = todo.Id ?? NewId.NextGuid(),
                todo.Name,
                todo.Description,
                todo.Done,
                todo.CreatorUserId,
            });

            return Accepted(commandId);
        }

        /// <summary>
        /// Delete todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CommandId</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteTodoAsync([FromRoute] Guid id)
        {
            var commandId = await bus.SendCommandAsync<DeleteTodo>(new
            {
                Id = id,
            });

            return Accepted(commandId);
        }

        /// <summary>
        /// Complete todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CommandId</returns>
        [HttpPut("{id}/complete")]
        public async Task<ActionResult<Guid>> CompleteTodoAsync([FromRoute] Guid id)
        {
            var commandId = await bus.SendCommandAsync<CompleteTodo>(new
            {
                Id = id,
            });

            return Accepted(commandId);
        }
    }
}