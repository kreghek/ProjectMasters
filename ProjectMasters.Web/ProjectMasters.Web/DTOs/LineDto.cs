namespace ProjectMasters.Web.DTOs
{
    using System.Collections.Generic;

    using Assets.BL;

    public class LineDto
    {
        public int Id { get; set; }
        public List<Person> Persons { get; set; }
    }
}