using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Provider;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        private readonly ITaskApplication _taskApplication;
        private readonly ICommentApplication _commentApplication;
        public TaskController(ITaskApplication taskApplication, ICommentApplication commentApplication, IUserIdProvider userIdProvider) : base(userIdProvider)
        {
            _commentApplication = commentApplication;
            _taskApplication = taskApplication;
        }

        #region Task
        [HttpGet("{projectId}")]
        public async Task<IActionResult> Get([FromRoute] int projectId)
        {
            var response = await _taskApplication.GetAsync(projectId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success(response.Object);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskCreateViewModel body)
        {
            var response = await _taskApplication.CreateAsync(body, userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskUpdateViewModel body)
        {
            var response = await _taskApplication.UpdateAsync(body, userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Remove([FromRoute] int taskId)
        {
            var response = await _taskApplication.RemoveAsync(taskId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }
        #endregion

        #region Comments
        [HttpGet("{taskId}/comment")]
        public async Task<IActionResult> GetComment([FromRoute] int taskId)
        {
            var response = await _commentApplication.GetAsync(taskId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success(response.Object);
        }

        [HttpPost("{taskId}/comment")]
        public async Task<IActionResult> PostComment([FromBody] CommentCreateViewModel body, [FromRoute] int taskId)
        {
            var response = await _commentApplication.CreateAsync(body, taskId, userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }

        [HttpPut("{taskId}/comment")]
        public async Task<IActionResult> PutComment([FromBody] CommentUpdateViewModel body, [FromRoute] int taskId)
        {
            var response = await _commentApplication.UpdateAsync(body, taskId, userId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }

        [HttpDelete("{taskId}/comment/{commentId}")]
        public async Task<IActionResult> RemoveComment([FromRoute] int taskId, [FromRoute] int commentId)
        {
            var response = await _commentApplication.RemoveAsync(taskId, commentId);
            if (response.IsInvalid)
                return Badrequest(response.Notifications);

            return Success();
        }
        #endregion
    }
}
