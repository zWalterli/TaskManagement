namespace TaskManagement.Domain.ViewModel
{
    public class CommentGetViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
