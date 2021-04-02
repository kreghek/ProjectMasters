﻿namespace Assets.BL
{
    public enum TraitType
    {
        /// <summary>
        /// Increase commit time twice but reduce chance to make error.
        /// </summary>
        CarefullDevelopment,

        /// <summary>
        /// Opposit Carefull development.
        /// </summary>
        RapidDevelopment,

        /// <summary>
        /// Increase skill up speed but increase error chance.
        /// </summary>
        FastLearning,

        /// <summary>
        /// Desrease skill up speed but decrease error chance.
        /// </summary>
        Apologet,

        /// <summary>
        /// Has more energy but low commit speed.
        /// </summary>
        Capacipator,

        /// <summary>
        /// Has less energy but high commit speed.
        /// </summary>
        Sprinter
    }
}
