using Microsoft.AspNetCore.Mvc;
using TestTwoAPI.Models;
using TestTwoAPI.Models.Extensions;
using TestTwoAPI.Repositories;

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
        public async Task<ActionResult<Sdk.Dtos.User>> Get([FromRoute] Guid id)
        {
            var user = await userRepository.GetAsync(id);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user.ToDto());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sdk.Dtos.User>>> GetAll()
        {
            var users = await userRepository.GetAllAsync();

            return Ok(users);
        }
    }
}