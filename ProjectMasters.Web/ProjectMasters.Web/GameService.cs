namespace ProjectMasters.Web
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class GameService
    {
        private readonly ConcurrentBag<Game> _games = new ConcurrentBag<Game>();

        public void AddGame(string user)
        {
            var game = new Game
            {
                User = user
            };

            _games.Add(game);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
                foreach (var game in _games.ToArray())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await game.UpdateAsync();
                }
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