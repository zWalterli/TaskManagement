using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Provider;
using TaskManagement.Application.Implementation;
using TaskManagement.Domain.Interface.Application;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly IReportApplication _reportApplication;
        public ReportController(IReportApplication reportApplication, IUserIdProvider userIdProvider) : base(userIdProvider)
        {
            _reportApplication = reportApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            var response = await _reportApplication.GetAmountTasksFinishedPerUserInAMounth();
            if (response.IsInvalid)
                return Badrequest(response.Notifications.Select(x => x.Message));

            return Success(response.Object);
        }
    }

}