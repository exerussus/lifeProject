using UnityEngine;

namespace src.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewGameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject
    {
        public CreatureData playerData;
        public CreatureData plaintData;
    }
}