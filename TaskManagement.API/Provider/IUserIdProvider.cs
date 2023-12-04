namespace TaskManagement.API.Provider
{
    public interface IUserIdProvider
    {
        int GetUserId(HttpContext httpContextAccessor);
    }
}
