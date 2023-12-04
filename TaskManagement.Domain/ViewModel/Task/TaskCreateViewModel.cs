using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.ViewModel
{
    public class TaskCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PriorityEnum Priority { get; set; }
        public int ProjectId { get; set; }
    }
}
