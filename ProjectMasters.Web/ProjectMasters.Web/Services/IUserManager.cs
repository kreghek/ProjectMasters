namespace ProjectMasters.Web.Services
{
    public interface IUserManager
    {
        string GetConnectionIdByUserId(string userId);
        string GetUserIdByConnectionId(string connectionId);
        void AddUserConnection(string connectionId, string userId);
        void RemoveUserConnection(string connectionId);
    }
}