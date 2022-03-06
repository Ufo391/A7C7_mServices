using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTodoService.Models;
using TestTodoService.Models.Extensions;

namespace TestTodoService.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> logger;
        private readonly TodoDbContext todoDbContext;

        public TodosController(
            ILogger<TodosController> logger,
            TodoDbContext todoDbContext)
        {
            this.logger = logger;
            this.todoDbContext = todoDbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Sdk.Dtos.Todo>>> Get([FromRoute] Guid id)
        {
            try
            {
                var todo = await todoDbContext.Todos.FirstAsync(x => x.Id == id);

                return Ok(todo.ToDto());
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());

                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sdk.Dtos.Todo>>> GetAll()
        {
            try
            {
                var todos = await todoDbContext.Todos
                    .Select(x => x.ToDto())
                    .ToListAsync();

                return Ok(todos);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());

                return Problem(e.Message);
            }
        }
    }
}