using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.DbModel
{
    public class ProjectDbModel : BaseDbModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        //
        public virtual IList<TaskDbModel> Tasks { get; set; }
    }
}
