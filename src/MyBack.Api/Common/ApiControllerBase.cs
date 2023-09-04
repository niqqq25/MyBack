using Microsoft.AspNetCore.Mvc;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Common;

[ApiController]
// [ApiExceptionFilter]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender _sender;
    
    protected ApiControllerBase(ISender sender)
    {
        _sender = sender;
    }
}