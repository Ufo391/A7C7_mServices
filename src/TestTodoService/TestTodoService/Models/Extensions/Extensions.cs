using MassTransit;

namespace TestTodoService.Models.Extensions
{
    public static class Extensions
    {
        public static Sdk.Dtos.Todo ToDto(this Todo todo)
        {
            return new Sdk.Dtos.Todo
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                Done = todo.Done,
                CreatorUserId = todo.CreatorUserId,
                CreatedAtUtc = todo.CreatedAtUtc,
                UpdatedAtUtc = todo.UpdatedAtUtc
            };
        }

        public static Todo ToModel(this Sdk.Dtos.Todo todo)
        {
            if (todo is null) throw new ArgumentNullException($"Cannot map dto to model");

            return new Todo
            {
                Id = todo.Id ?? NewId.NextGuid(),
                Name = todo.Name,
                Description = todo.Description,
                Done = todo.Done,
                CreatorUserId = todo.CreatorUserId,
                CreatedAtUtc = todo.CreatedAtUtc,
                UpdatedAtUtc = todo.UpdatedAtUtc
            };
        }

        public static Todo ToModel(this Sdk.Commands.AddTodo todo)
        {
            var dateNowUtc = DateTime.UtcNow;

            return new Todo
            {
                Id = todo.Id ?? NewId.NextGuid(),
                Name = todo.Name,
                Description = todo.Description,
                Done = todo.Done,
                CreatorUserId = todo.CreatorUserId,
                CreatedAtUtc = dateNowUtc,
                UpdatedAtUtc = dateNowUtc
            };
        }
    }
}
