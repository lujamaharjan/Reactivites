using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasicApiController(IMediator mediator) : ControllerBase
{
        protected IMediator MediatorObj { get; } = mediator;
}