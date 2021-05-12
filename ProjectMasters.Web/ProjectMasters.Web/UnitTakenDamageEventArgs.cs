namespace ProjectMasters.Games
{
    using System;

    using Web.DTOs;

    public class UnitTakenDamageEventArgs : EventArgs
    {
        public UnitTakenDamageEventArgs(UnitDto unitDto)
        {
            UnitDto = unitDto;
        }

        public UnitDto UnitDto { get; }
    }
}