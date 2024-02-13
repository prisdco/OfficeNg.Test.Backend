using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.Test.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<BaseController> _logger;

      
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected ILogger<BaseController> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<BaseController>>());

       
    }
}
