namespace TaskManagement.API.Provider
{
    public class UserIdProvider : IUserIdProvider
    {
        public int GetUserId(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower().Equals("userid"));

            if (string.IsNullOrEmpty(header.Value) || int.Parse(header.Value) <= 0)
                throw new NullReferenceException("User ID is invalid!");

            return int.Parse(header.Value);
        }
    }
}