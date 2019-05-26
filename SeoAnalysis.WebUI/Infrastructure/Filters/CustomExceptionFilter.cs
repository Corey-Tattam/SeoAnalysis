using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SeoAnalysis.Core.Exceptions;
using System;
using System.Net;

namespace SeoAnalysis.WebUI.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        #region " - - - - - - Methods - - - - - - "

        public override void OnException(ExceptionContext context)
        {
            // The result of all exceptions will be a JsonResult.
            context.HttpContext.Response.ContentType = "application/json";

            switch (context.Exception)
            {
                case ValidationException ve:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(new { ve.Message });
                    break;

                default:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Result = new JsonResult(new
                    {
                        context.Exception.Message,
                        context.Exception.StackTrace
                    });
                    break;
            }
        } //OnException

        #endregion //Methods

    } //CustomExceptionFilterAttribute
}
