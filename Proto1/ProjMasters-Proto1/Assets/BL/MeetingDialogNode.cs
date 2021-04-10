using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BL
{
    class MeetingDialogNode
    {
        public string SpeakerName { get; set; }
        public string Text { get; set; }
        public bool IsEndNode { get; set; }
    }

    class MeetingAnswer
    { 
        public string Text { get; set; }

        public Person AddNewPerson { get; set; }
    }
}
