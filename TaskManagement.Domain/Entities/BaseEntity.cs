using Flunt.Notifications;

namespace TaskManagement.Domain.Entities
{
    public class BaseEntity : Notifiable<Notification>
    {
        public int? Id { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserIdCreated { get; set; }
        public int? UserIdLastUpdate { get; set; }
    }
}