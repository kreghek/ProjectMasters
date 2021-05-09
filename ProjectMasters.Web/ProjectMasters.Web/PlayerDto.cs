namespace ProjectMasters.Games
{
    using System;
    using System.Linq;

    using Assets.BL;

    public class PlayerDto
    {
        public int Authority { get; set; }

        public int Money { get; set; }

        public string Percent { get; set; }

        public PlayerDto()
        {
            Money = Player.Money;
            Authority = Player.Autority;

            var units = GameState.Project.Lines.SelectMany(x => x.Units);
            var solved = units.Sum(x => x.TimeLog);
            var remaining = units.Sum(x => x.Cost);
            var percent = Math.Round(solved / remaining * 100, MidpointRounding.ToEven);
            Percent = percent.ToString();
        }
    }
}