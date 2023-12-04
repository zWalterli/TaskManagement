namespace TaskManagement.Domain.DbModel
{
    public class LogDbModel : BaseDbModel
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}