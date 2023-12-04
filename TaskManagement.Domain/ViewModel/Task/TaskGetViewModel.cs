using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.ViewModel
{
    public class TaskGetViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusTaskEnum Status { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public virtual List<CommentGetViewModel> Comments { get; set; }
    }
}
