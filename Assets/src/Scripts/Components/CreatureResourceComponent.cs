using System;
using src.Scripts.Logic;

namespace src.Scripts.Components
{
    [Serializable]
    public struct CreatureResourceComponent
    {
        public CreatureResource health;
        public CreatureResource stamina;
    }
}