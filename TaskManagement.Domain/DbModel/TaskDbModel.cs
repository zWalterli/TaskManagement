using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.DbModel
{
    public class TaskDbModel : BaseDbModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTaskEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        //
        public virtual ProjectDbModel Project { get; set; }
        public virtual List<CommentDbModel> Comments { get; set; }
        
    }
}
