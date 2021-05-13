namespace ProjectMasters.Web.Services
{
    public interface IUserManager
    {
        void AddUserConnection(string connectionId, string userId);
        string GetConnectionIdByUserId(string userId);
        string GetUserIdByConnectionId(string connectionId);
        void RemoveUserConnection(string connectionId);
    }
}