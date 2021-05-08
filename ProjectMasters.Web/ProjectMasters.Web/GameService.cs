using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMasters.Web
{
    public class GameService
    {
        private ConcurrentBag<Game> _games = new ConcurrentBag<Game>();

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                foreach (var game in _games.ToArray())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await game.UpdateAsync();
                }
            }
        }

        public void AddGame(string user)
        {
            var game = new Game
            {
                User = user
            };

            _games.Add(game);
        }
    }

    public class Game
    { 
        public string User { get; set; }

        internal Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
