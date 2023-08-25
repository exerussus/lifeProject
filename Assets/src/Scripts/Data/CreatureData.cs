
using src.Scripts.Logic;
using UnityEngine;

namespace src.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewCreatureData", menuName = "Data/CreatureData")]
    public class CreatureData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float moveSpeed;
        [Range(50f, 200f)][SerializeField] private float rotationSpeed;
        [SerializeField] private int maxSatiety;
        [SerializeField] private CreatureType creatureType;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        [SerializeField] private int fraction;
        [Range(1f, 10f)][SerializeField] private float rangeOfSight;
        [Range(1f, 300f)][SerializeField] private float lineOfSight;

        public GameObject Prefab => prefab;
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
        public int MaxSatiety => maxSatiety;
        public CreatureType CreatureType => creatureType;
        public float MaxHealth => maxHealth;
        public float MaxStamina => maxStamina;
        public int Fraction => fraction;
        public float RangeOfSight => rangeOfSight;
        public float LineOfSight => lineOfSight;
    }
}
