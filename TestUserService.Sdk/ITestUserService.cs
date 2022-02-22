using Refit;

namespace TestTwoAPI.Sdk
{
    public interface ITestUserService
    {
        /// <summary>
        /// Get single user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/users/{id}")]
        Task<Dtos.User> GetUser(Guid id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Get("/users")]
        Task<IEnumerable<Dtos.User>> GetAllUsers();
    }
}
