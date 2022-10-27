using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.AppUser.CreateUser;

namespace MiniECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandReuquest createUserCommandReuquest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandReuquest);
            return Ok(response);
        }
    }
}
