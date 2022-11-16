using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.AppUser.CreateUser;
using MiniECommerce.Application.Features.Commands.AppUser.UpdatePassword;

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
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommadRequest updatePasswordCommadRequest)
        {
            UpdatePasswordCommadResponse response = await _mediator.Send(updatePasswordCommadRequest);
            return Ok(response);    
        }
    }
}
