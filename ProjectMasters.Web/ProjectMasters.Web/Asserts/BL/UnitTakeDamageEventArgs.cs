using System;

namespace Assets.BL
{
    public class UnitTakeDamageEventArgs : EventArgs
    {
        public float Damage { get; set; }
        public bool IsCrit { get; set; }
    }
}