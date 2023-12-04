using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Provider;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectApplication _projectApplication;
        public ProjectController(IProjectApplication projectApplication, IUserIdProvider userIdProvider) : base(userIdProvider)
        {
            _projectApplication = projectApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _projectApplication.GetAsync(userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success(response.Object);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectCreateViewModel body)
        {
            var response = await _projectApplication.CreateAsync(body, userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> Remove([FromRoute] int projectId)
        {
            var response = await _projectApplication.RemoveAsync(projectId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }
    }
}
