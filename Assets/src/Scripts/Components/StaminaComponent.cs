
using System;
using UnityEngine;

namespace src.Scripts.Components
{
    [Serializable]
    public struct StaminaComponent
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _value;
        
        public float NormalizedValue => _value / _maxValue;

        public bool IsEmpty
        {
            get
            {
                if (!(_maxValue > 0)) return false;
                return _value <= 0;
            }
        }
        
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if (!(value > 0)) return;
                _maxValue = value;
                _value = value;
            }
        }

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_value > _maxValue) _value = _maxValue;
            }
        }
    }
}