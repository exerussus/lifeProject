using System;
using Leopotam.EcsLite;

namespace src.Scripts.Components
{
    [Serializable]
    public struct PreyComponent : IEcsAutoReset<PreyComponent>
    {
        public int entity;
        public float huntingTime;
        public const float MaxHuntingTime = 4f;
        
        public void AutoReset (ref PreyComponent c)
        {
            c.huntingTime = MaxHuntingTime;
        }
    }
}