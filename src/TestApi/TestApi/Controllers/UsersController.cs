using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TestUserService.Sdk;
using TestUserService.Sdk.Commands;
using TestUserService.Sdk.Dtos;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ITestUserService testUserService;
        private readonly IBus bus;

        public UsersController(
            ITestUserService testUserService,
            IBus bus)
        {
            this.testUserService = testUserService;
            this.bus = bus;
        }

        /// <summary>
        /// Get single user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get([FromRoute] Guid id)
        {
            var user = await testUserService.GetUserAsync(id);

            return Ok(user);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var user = await testUserService.GetAllUsersAsync();

            return Ok(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>CommandId</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUserAsync([FromBody] User user)
        {
            var commandId = await bus.SendCommandAsync<CreateUser>(new
            {
                Id = user.Id ?? NewId.NextGuid(),
                user.GivenName,
                user.FamilyName
            });

            return Accepted(commandId);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CommandId</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteUserAsync([FromRoute] Guid id)
        {
            var commandId = await bus.SendCommandAsync<DeleteUser>(new
            {
                Id = id,
            });

            return Accepted(commandId);
        }
    }
}