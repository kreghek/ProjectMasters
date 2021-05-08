using Microsoft.AspNetCore.SignalR;

using ProjectMasters.Games;

namespace ProjectMasters.Web.Hubs
{
    public class GameHub : Hub<IGame>
    {
    }
}