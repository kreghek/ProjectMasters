namespace ProjectMasters.Web.DTOs
{
    public class UnitDto
    {
        public int Id { get; set; }

        public int LineId { get; set; }

        public string Type { get; set; }
        public int QueueIndex { get; internal set; }
    }
}