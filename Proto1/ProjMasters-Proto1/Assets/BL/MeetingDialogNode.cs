namespace Assets.BL
{
    public class MeetingDialogNode
    {
        public string SpeakerName { get; set; }
        public string Text { get; set; }
        public bool IsEndNode { get; set; }

        public MeetingAnswer[] Answers { get; set; }
    }
}
