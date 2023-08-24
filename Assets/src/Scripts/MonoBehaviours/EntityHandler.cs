using UnityEngine;

namespace src.Scripts.MonoBehaviours
{
    public class EntityHandler : MonoBehaviour
    {
        [SerializeField] private int _id;
        public int ID => _id;

        public void Init(int value)
        {
            _id = value;
        }
    }
}