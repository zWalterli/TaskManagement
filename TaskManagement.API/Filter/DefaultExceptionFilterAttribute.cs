using System.Net;
using TaskManagement.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagement.API.Filter
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DEFAULT_EXCEPTION = "An unexpected error has occurred!";
        public override void OnException(ExceptionContext context)
        {
            if (context is null)
                return;

            if (context.Exception is OperationCanceledException)
            {
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
                return;
            }

            var erros = new List<string> { context.Exception.Message };
            var response = new ResponseViewModel(false, DEFAULT_EXCEPTION, null, erros);
            context.Result = new ObjectResult(response)
            {
                StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
            };
        }
    }
}