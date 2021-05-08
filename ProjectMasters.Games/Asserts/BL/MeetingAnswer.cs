using System.Linq;

namespace Assets.BL
{
    public abstract class MeetingAnswer
    { 
        public string Text { get; set; }

        public MeetingDialogNode NextDialogNode { get; set; }

        public void Apply()
        {
            Player.MeetingNode = NextDialogNode;

            ApplyInner();

            if (Player.MeetingNode.IsEndNode)
            {
                Player.ProjectLevel = Player.MeetingNode.ProjectLevel;
            }
        }

        protected abstract void ApplyInner();
    }

    public class AddNewPersonAnswer : MeetingAnswer
    {
        public Person NewPerson { get; set; }

        protected override void ApplyInner()
        {
            Team.Persons = Team.Persons.Concat(new[] { NewPerson }).ToArray();
        }
    }
}
