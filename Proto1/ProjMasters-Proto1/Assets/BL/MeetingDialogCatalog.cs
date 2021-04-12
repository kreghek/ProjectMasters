using System;

namespace Assets.BL
{
    public static class MeetingDialogCatalog
    {
        public static MeetingDialogNode[] DialogNodes = new[] {
            new MeetingDialogNode{
                Text = "[The project has been completed. The boss summoned you to the carpet.] You've done well. We need people like that. Now we have a \"burning\" project that should have been done yesterday. Take a promising newbie to help you. This is the beloved nephew of a very respected person in our corporation.",
                SpeakerName = "Angela Mercedes",
                Answers = new MeetingAnswer[] { 
                    new MeetingAnswer
                    { 
                        Text = "Ok",
                        AddNewPerson = new Person
                        {
                            Name = "Peter Gipsi",
                            Traits = Array.Empty<TraitType>(),
                            Skills = Array.Empty<Skill>(),
                            EyeIndex = 2,
                            FaceDecorIndex = 0,
                        },
                        NextDialogNode = new MeetingDialogNode
                        { 
                            Text = "Good. This problem now is not my problem.",
                            IsEndNode = true
                        }
                    }
                }
            }
        };
    }
}
