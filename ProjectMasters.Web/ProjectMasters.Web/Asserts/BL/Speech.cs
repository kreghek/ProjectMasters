namespace Assets.BL
{
    using System;

    public class Speech
    {
        public const float SPEECH_COUNTER = 20;
        public ISpeechSource Source { get; set; }

        public string Text { get; set; }
    }

    public interface ISpeechSource
    {
    }

    public static class SpeechPool
    {
        public static void AddSpeech(Speech speech)
        {
            SpeechAdded?.Invoke(null, new SpeechAddedEventArgs { Speech = speech });
        }

        public static event EventHandler<SpeechAddedEventArgs> SpeechAdded;
    }

    public class SpeechAddedEventArgs : EventArgs
    {
        public Speech Speech { get; set; }
    }

    public static class SpeechCatalog
    {
        public static string[] BadPersonSpeeches =
        {
            "I can't do done it!",
            "This expedition is doomed...",
            "Please, kill me"
        };

        public static string[] UnitTauntSpeeches =
        {
            "I'll kill you!",
            "That is too weak. Next!",
            "Come on! Crush me up!",
            "You can't to win!",
            "Surrender!",
            "And we know no fear!"
        };
    }
}