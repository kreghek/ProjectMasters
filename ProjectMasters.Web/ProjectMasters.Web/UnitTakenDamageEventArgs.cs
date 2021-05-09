namespace ProjectMasters.Games
{
    using System;

    using ProjectMasters.Web.DTOs;

    public class UnitTakenDamageEventArgs : EventArgs
    {
        public UnitTakenDamageEventArgs(UnitDto unitDto)
        {
            UnitDto = unitDto;
        }

        public UnitDto UnitDto { get; }
    }
}