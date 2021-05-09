namespace ProjectMasters.Games
{
    using System.Linq;

    using Assets.BL;

    public class PlayerDto
    {
        public int Auhtority;

        public int Money;

        public float Percent;

        public PlayerDto()
        {
            Money = Player.Money;
            Auhtority = Player.Autority;

            var units = GameState.Project.Lines.SelectMany(x => x.Units);
            var solved = units.Sum(x => x.TimeLog);
            var remaining = units.Sum(x => x.Cost);
            Percent = solved / remaining * 100;
        }
    }
}