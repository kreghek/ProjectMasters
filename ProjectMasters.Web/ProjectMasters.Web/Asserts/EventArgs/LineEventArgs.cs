namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class LineEventArgs : EventArgs
    {
        public LineEventArgs(ProjectLine line)
        {
            Line = line ?? throw new ArgumentNullException(nameof(line));
        }

        public ProjectLine Line { get; }
    }
}