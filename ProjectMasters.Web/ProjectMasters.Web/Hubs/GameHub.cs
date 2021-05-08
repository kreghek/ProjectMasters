using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace ProjectMasters.Web.Hubs
{
    public class GameHub : Hub
    {
        //private readonly GameService _gameService;

        //public GameHub(GameService gameService)
        //{
        //    _gameService = gameService;
        //}

        public async Task StartGameAsync(string message)
        {
            await this.Clients.All.SendAsync("update", message);
            //return Task.Run(() =>
            //{
            //    _gameService.AddGame(user);
            //});
        }
    }
}