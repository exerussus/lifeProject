
using src.Scripts.Components;
using src.Scripts.Data;
using UnityEngine;

namespace src.Scripts.Logic
{
    public static class CreateCreature
    {
        public static GameObject Instantiate(
            CreatureData creatureData, 
            Transform parentTransform,
            ref TransformComponent transformComponent,
            ref MoveSpeedComponent moveSpeedComponent,
            ref SatietyComponent satietyComponent,
            ref CreatureTypeComponent creatureTypeComponent
            )
        {
            var createdGameObject = Object.Instantiate(creatureData.Prefab, parentTransform);
            
            transformComponent.transform = createdGameObject.transform;
            moveSpeedComponent.value = creatureData.MoveSpeed;
            satietyComponent.satiety.MaxValue = creatureData.MaxSatiety;
            creatureTypeComponent.creatureType = creatureData.CreatureType;
            
            return createdGameObject; 
        }
    }
}