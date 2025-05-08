using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;
}