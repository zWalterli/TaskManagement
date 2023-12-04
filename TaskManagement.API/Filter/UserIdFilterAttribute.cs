using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Domain.ViewModel;

namespace TaskManagement.API.Filter
{
    public class UserIdFilterAttribute : ActionFilterAttribute
    {
        private readonly int _userId;
        public UserIdFilterAttribute(int clientId)
        {
            _userId = clientId;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request != null)
            {
                var localUserId = _userId;
                if (!filterContext.HttpContext.Request.Headers.ContainsKey("userId"))
                {
                    var erros = new List<string> { "User Id is empty!" };
                    var response = new ResponseViewModel(false, "User Id empty!", null, erros);
                    filterContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult(response);
                    return;
                }

                var userId = int.Parse(filterContext.HttpContext.Request.Headers["userId"].ToString());
                if (localUserId != userId)
                {
                    var erros = new List<string> { "User Id is invalid!" };
                    var response = new ResponseViewModel(false, "User Id is invalid!", null, erros);
                    filterContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult(response);
                    return;
                }
            }
        }
    }
}
