using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gatherer.Api.Controllers
{
    [Route("{settlementId}/[controller]")]
    [Authorize(Policy = "user")]
    public class ExpensesController : ApiControllerBase
    {
        
    }
}