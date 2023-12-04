namespace TaskManagement.Domain.DbModel
{
    public class CommentDbModel : BaseDbModel
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        //
        public virtual TaskDbModel Task { get; set; }
    }
}
