using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTodoService.Sdk
{
    public interface ITestTodoService
    {
        /// <summary>
        /// Get single todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/api/todos/{id}")]
        Task<Dtos.Todo> GetTodoAsync(Guid id);

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        [Get("/api/todos")]
        Task<IEnumerable<Dtos.Todo>> GetAllTodosAsync();
    }
}
