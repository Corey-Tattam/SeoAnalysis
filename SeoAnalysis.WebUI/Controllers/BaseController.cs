using Microsoft.AspNetCore.Mvc;
using SeoAnalysis.WebUI.Infrastructure.Filters;

namespace SeoAnalysis.WebUI.Controllers
{
    [ApiController]
    [CustomExceptionFilter]
    public abstract class BaseController : ControllerBase
    {
    } //BaseController
}
