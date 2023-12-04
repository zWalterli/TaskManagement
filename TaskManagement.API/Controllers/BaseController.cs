using TaskManagement.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using TaskManagement.API.Provider;

namespace TaskManagement.API.Controllers
{
    [ExcludeFromCodeCoverage]
    public class BaseController : ControllerBase
    {
        private readonly IUserIdProvider userIdProvider;
        public BaseController(IUserIdProvider userIdProvider)
        {
            this.userIdProvider = userIdProvider;
        }
        protected int userId => userIdProvider.GetUserId(HttpContext);

        protected OkObjectResult Success() => Ok(new ResponseViewModel());
        protected OkObjectResult Success<T>(T data, string? message = "Operation finally successfuly!")
            => Ok(new ResponseViewModel<T>(true, message, data));
        protected OkObjectResult Success(object data, string? message = "Operation finally successfuly")
            => Ok(new ResponseViewModel(true, message, data));
        protected BadRequestObjectResult Badrequest<T>(T data, string? message = "An error ocorred while performing the operation!")
            => BadRequest(new ResponseViewModel<T>(false, message, data));
        protected BadRequestObjectResult Badrequest(List<string> data, string? message = "An error ocorred while performing the operation!")
            => BadRequest(new ResponseViewModel<string>(false, message, null, data));
    }
}