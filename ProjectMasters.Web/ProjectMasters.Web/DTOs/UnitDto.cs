namespace ProjectMasters.Web.DTOs
{
    using Assets.BL;

    public class UnitDto
    {
        public UnitDto(ProjectUnitBase unit)
        {
            Id = unit.Id;
            LineId = unit.LineIndex;
            Type = unit.Type.ToString();
            QueueIndex = unit.QueueIndex;
            RequiredMasteryItems = unit.RequiredMasteryItems;
        }

        public int Id { get; set; }

        public int LineId { get; set; }
        public int QueueIndex { get; internal set; }
        public string[] RequiredMasteryItems { get; set; }

        public string Type { get; set; }
    }
}