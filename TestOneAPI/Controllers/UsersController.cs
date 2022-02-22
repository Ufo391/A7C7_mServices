using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TestTwoAPI.Sdk;
using TestTwoAPI.Sdk.Commands;
using TestTwoAPI.Sdk.Dtos;

namespace TestOneAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var user = await testUserService.GetUser(id);

            return Ok(user);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var user = await testUserService.GetAllUsers();

            return Ok(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>CommandId</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateVoid([FromBody] User user)
        {
            var userId = user.Id ?? NewId.NextGuid();

            await bus.Publish<CreateUser>(new
            {
                Id = userId,
                user.GivenName,
                user.FamilyName
            });

            return Accepted(userId);
        }
    }
}