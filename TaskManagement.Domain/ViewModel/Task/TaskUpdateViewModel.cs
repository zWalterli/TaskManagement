using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.ViewModel
{
    public class TaskUpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTaskEnum Status { get; set; }
    }
}
