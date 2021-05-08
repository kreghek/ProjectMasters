namespace ProjectMasters.Web.DTOs
{
    using System.Collections.Generic;

    using Assets.BL;

    public class PersonDto
    {
        public int Id { get; set; }
        public List<Mastery> MasteryLevels { get; set; }

        public Skill[] Skills { get; set; }

        public PersonDto(Person person)
        {
            Id = person.Id;
            MasteryLevels = person.MasteryLevels;
            Skills = person.Skills;
        }
    }
}