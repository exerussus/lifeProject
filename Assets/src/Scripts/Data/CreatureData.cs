
using src.Scripts.Logic;
using UnityEngine;

namespace src.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewCreatureData", menuName = "Data/CreatureData")]
    public class CreatureData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int maxSatiety;
        [SerializeField] private CreatureType creatureType;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        [SerializeField] private int fraction;

        public GameObject Prefab => prefab;
        public float MoveSpeed => moveSpeed;
        public int MaxSatiety => maxSatiety;
        public CreatureType CreatureType => creatureType;
        public float MaxHealth => maxHealth;
        public float MaxStamina => maxStamina;
        public int Fraction => fraction;
    }
}
