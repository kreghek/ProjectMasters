using System;

namespace Assets.BL
{
    public class Speech
    {
        public const float SPEECH_COUNTER = 20;

        public string Text { get; set; }
        public ISpeechSource Source { get; set; }
    }

    public interface ISpeechSource
    {
    }

    public static class SpeechPool
    {
        public static event EventHandler<SpeechAddedEventArgs> SpeechAdded;

        public static void AddSpeech(Speech speech)
        {
            SpeechAdded?.Invoke(null, new SpeechAddedEventArgs { Speech = speech });
        }
    }

    public class SpeechAddedEventArgs : EventArgs
    {
        public Speech Speech { get; set; }
    }

    public static class SpeechCatalog
    {
        public static string[] BadPersonSpeeches = new[] { 
            "I can't do done it!",
            "This expedition is doomed...",
            "Please, kill me"
        };

        public static string[] UnitTauntSpeeches = new[] {
            "I'll kill you!",
            "That is too weak. Next!",
            "Come on! Crush me up!",
            "You can't to win!",
            "Surrender!",
            "And we know no fear!"
        };
    }
}