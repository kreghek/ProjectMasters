namespace ProjectMasters.Web.DTOs
{
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BL;

    public class PersonDto
    {
        public PersonDto(Person person)
        {
            Id = person.Id;
            MasteryLevels = person.MasteryLevels;
            Skills = person.Skills;
            MouthIndex = person.MouthIndex;
            HairIndex = person.HairIndex;
            EyesIndex = person.EyesIndex;
            Name = person.Name;
            Traits = person.Traits.Select(x => x.ToString()).ToArray();
        }

        public int EyesIndex { get; set; }
        public int HairIndex { get; set; }
        public int Id { get; set; }
        public int? LineId { get; internal set; }
        public List<Mastery> MasteryLevels { get; set; }
        public int MouthIndex { get; set; }

        public string Name { get; set; }

        public Skill[] Skills { get; set; }
        public string[] Traits { get; set; }
    }
}