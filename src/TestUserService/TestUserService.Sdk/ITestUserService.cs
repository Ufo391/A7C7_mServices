using Refit;

namespace TestUserService.Sdk
{
    public interface ITestUserService
    {
        /// <summary>
        /// Get single user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/api/users/{id}")]
        Task<Dtos.User> GetUserAsync(Guid id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Get("/api/users")]
        Task<IEnumerable<Dtos.User>> GetAllUsersAsync();
    }
}
