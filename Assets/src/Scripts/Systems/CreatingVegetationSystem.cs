using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class CreatingVegetationSystem : EcsInit
    {
        private readonly EcsPoolInject<VegetationMark> _vegetationPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;
        private readonly EcsPoolInject<CreatureMark> _creaturePool;
        private readonly EcsPoolInject<FractionComponent> _fractionPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<VegetationMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            var newEntity = _world.NewEntity();
            
            _creaturePool.Value.Add(newEntity);
            _vegetationPool.Value.Add(newEntity);
            ref var transformComponent = ref _transformPool.Value.Add(newEntity);
            ref var fractionComponent = ref _fractionPool.Value.Add(newEntity);
            var createdGameObject = Object.Instantiate(_gameData.plaintData.Prefab);
            var entityHandler = createdGameObject.AddComponent<EntityHandler>();
            entityHandler.Init(newEntity);
            transformComponent.transform = createdGameObject.transform;
            fractionComponent.value = 1;
        }
    }
}