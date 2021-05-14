namespace ProjectMasters.Web.Services
{
    /// <summary>
    /// Stores all mappings between user id and connection id and allows to perform convertion.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Add new mapping between user id and connection id.
        /// </summary>
        /// <param name="connectionId"> The unique SignalR connection id. </param>
        /// <param name="userId"> The unique user id from the client. </param>
        void AddUserConnection(string connectionId, string userId);

        /// <summary>
        /// Gets the connection id of specified user id.
        /// </summary>
        /// <param name="userId"> The user id from client. </param>
        /// <returns> The SignalR connection id. </returns>
        string GetConnectionIdByUserId(string userId);

        /// <summary>
        /// Gets the user id of specified connection id.
        /// </summary>
        /// <param name="connectionId"> The SignalR connection id from a hub context. </param>
        /// <returns> The user id mapped to the connection. </returns>
        string GetUserIdByConnectionId(string connectionId);

        /// <summary>
        /// Removes connection data from the storage.
        /// </summary>
        /// <param name="connectionId"> The SignalR connection id from the hub context. </param>
        void RemoveUserConnection(string connectionId);
    }
}