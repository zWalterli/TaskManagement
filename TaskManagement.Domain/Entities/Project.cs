using TaskManagement.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        //
        public virtual IList<Task>? Tasks { get; set; }

        public bool IsValidToCreate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Title, "Title")
                .IsNotNullOrEmpty(Description, "Description")
            );

            return !Notifications.Any();
        }
    }
}
