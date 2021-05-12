namespace ProjectMasters.Games
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Assets.BL;

    public class PlayerDto
    {
        public PlayerDto(GameState gameState)
        {
            Money = gameState.Player.Money;
            Authority = gameState.Player.Autority;

            var units = gameState.Project.Lines.SelectMany(x => x.Units);
            var bases = units as ProjectUnitBase[] ?? units.ToArray();
            var solved = bases.Sum(x => x.TimeLog);
            var remaining = bases.Sum(x => x.Cost);
            var percent = Math.Round(solved / remaining * 100, MidpointRounding.ToEven);
            Percent = percent.ToString(CultureInfo.InvariantCulture);
        }

        public int Authority { get; set; }

        public int Money { get; set; }

        public string Percent { get; set; }
    }
}