
using System;
using UnityEngine;

namespace src.Scripts.Components
{
    [Serializable]
    public struct MoveStateComponent
    {
        public bool required;
        public Vector2 direction;
    }
}