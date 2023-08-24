
using System;
using System.Collections.Generic;
using src.Scripts.MonoBehaviours;


namespace src.Scripts.Components
{
    [Serializable]
    public struct MemoryComponent
    {
        public List<EntityHandler> detectedEntities;
    }
}