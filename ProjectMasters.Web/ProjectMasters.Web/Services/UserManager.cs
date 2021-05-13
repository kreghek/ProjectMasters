using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ProjectMasters.Web.Services
{
    public class UserManager : IUserManager
    {
        private readonly ConcurrentDictionary<string, string> _userIdDict;

        public UserManager()
        {
            _userIdDict = new ConcurrentDictionary<string, string>();
        }

        public string GetUserIdByConnectionId(string connectionId)
        {
            if (!_userIdDict.TryGetValue(connectionId, out var userId))
            {
                throw new InvalidOperationException("There is no connection id to map it to user id.");
            }

            return userId;
        }

        public string GetConnectionIdByUserId(string userId)
        {
            var connectionIds = _userIdDict.Where(x => x.Value == userId).Select(x => x.Key);
            if (connectionIds.Count() > 1)
            {
                // This error occured then a connectionId has not been removed from storage (inner dictionary)
                // after real signalR connection was lost.
                throw new InvalidOperationException("There are more that 1 user connection.");
            }

            return connectionIds.SingleOrDefault();
        }

        public void AddUserConnection(string connectionId, string userId)
        {
            if (!_userIdDict.TryAdd(connectionId, userId))
            {
                throw new InvalidOperationException("Mapping of connection id and user id failed.");
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            if (!_userIdDict.TryRemove(connectionId, out var _))
            {
                throw new InvalidOperationException("Error during connection removing.");
            }
        }
    }
}