namespace ProjectMasters.Web.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public int? LineId { get; internal set; }
        public int EyesIndex { get; set; }
        public int HairIndex { get; set; }
        public int MouthIndex { get; set; }
    }
}