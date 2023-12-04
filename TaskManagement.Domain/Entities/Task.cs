using TaskManagement.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTaskEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        //
        public virtual Project Project { get; set; }
        public virtual List<Comment> Comments { get; set; }
        

        public bool IsValidToCreate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Title, "Title")
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterThan(UserId, 0, "UserId")
                .IsGreaterThan(ProjectId, 0, "ProjectId")
            );

            return !Notifications.Any();
        }

        public void SetEntityToCreateTask()
        {
            Status = StatusTaskEnum.ToDo;
            Priority = Priority == 0 ? PriorityEnum.Low : Priority;
        }

        public bool IsValidToUpdate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Title, "Title")
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterThan((int)Status, 0, "Status")
                .IsGreaterThan(Id ?? 0, 0, "Id")
            );

            return !Notifications.Any();
        }
    }
}
