
using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using src.Scripts.MonoBehaviours;


namespace src.Scripts.Components 
{
    [Serializable]
    public struct MemoryComponent : IEcsAutoReset<MemoryComponent>
    {
        public List<EntityHandler> detectedEntities;
        
        public void AutoReset (ref MemoryComponent c)
        {
            c.detectedEntities = new List<EntityHandler>();
        }
    }
    
   
}