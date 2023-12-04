using Flunt.Notifications;

namespace TaskManagement.Domain.ViewModel.Result
{
    public class Result : Notifiable<Notification>
    {
        public bool IsInvalid => !IsValid;
        protected Result()
        {
        }

        protected Result(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result(notifications);
        }

        public static Result Error(string message, string? title = "")
        {
            return new Result(
                new List<Notification>()
                {
                    new Notification(title, message)
                }
            );
        }

        public static Result Error(Notification notification)
        {
            return new Result(new List<Notification> { notification });
        }
    }


    public class Result<T> : Notifiable<Notification> where T : class
    {
        public T? Object { get; }
        public bool IsInvalid => !IsValid;

        private Result(T obj)
        {
            Object = obj;
        }

        private Result(IReadOnlyCollection<Notification> notifications)
        {
            Object = null;
            AddNotifications(notifications);
        }

        public static Result<T> Ok(T obj)
        {
            return new Result<T>(obj);
        }

        public static Result<T> Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result<T>(notifications);
        }

        public static Result<T> Error(string message, string? title = "")
        {
            return new Result<T>(
                new List<Notification>()
                {
                    new Notification(title, message)
                }
            );
        }
    }
}