using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController(IMediator mediator) : ControllerBase
{
        private IMediator _mediator;
        protected IMediator MediatorObj => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result is null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.Error);
        }

}