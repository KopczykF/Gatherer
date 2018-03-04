using System;
using Gatherer.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Gatherer.Api.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher _commandDispatcher;

        protected Guid CurrentUserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher =commandDispatcher;
        }
    }
}