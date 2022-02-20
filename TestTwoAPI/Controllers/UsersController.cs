using Microsoft.AspNetCore.Mvc;
using TestUserService.Models;
using TestUserService.Models.Extensions;
using TestUserService.Repositories;

namespace TestTwoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestUserService.Sdk.Dtos.User>> Get([FromRoute] Guid id)
        {
            var user = await userRepository.GetAsync(id);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user.ToDto());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestUserService.Sdk.Dtos.User>>> GetAll()
        {
            var users = await userRepository.GetAllAsync();

            return Ok(users);
        }
    }
}