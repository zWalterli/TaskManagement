using Flunt.Notifications;
using Flunt.Validations;

namespace TaskManagement.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }

        public bool IsValidToCreate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterThan(UserId, 0, "UserId")
                .IsGreaterThan(TaskId, 0, "TaskId")
            );

            return !Notifications.Any();
        }

        public bool IsValidToUpdate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterThan(Id ?? 0, 0, "Id")
                .IsGreaterThan(UserId, 0, "UserId")
                .IsGreaterThan(TaskId, 0, "TaskId")
            );

            return !Notifications.Any();
        }
    }
}
