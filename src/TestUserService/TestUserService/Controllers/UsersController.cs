using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestUserService.Models;
using TestUserService.Models.Extensions;

namespace TestUserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext userDbContext;

        public UsersController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sdk.Dtos.User>> Get([FromRoute] Guid id)
        {
            var user = await userDbContext.Users.FindAsync(id);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user.ToDto());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sdk.Dtos.User>>> GetAll()
        {
            var users = await userDbContext.Users.ToListAsync();

            return Ok(users);
        }
    }
}