using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class CreatingVegetationSystem : EcsInit
    {
        private EcsPool<VegetationMark> _vegetationPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<CreatureMark> _creaturePool;
        private EcsPool<FractionComponent> _fractionPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<VegetationMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _vegetationPool = _world.GetPool<VegetationMark>();
            _transformPool = _world.GetPool<TransformComponent>();
            _creaturePool = _world.GetPool<CreatureMark>();
            _fractionPool = _world.GetPool<FractionComponent>();
            
            var newEntity = _world.NewEntity();
            
            _creaturePool.Add(newEntity);
            _vegetationPool.Add(newEntity);
            ref var transformComponent = ref _transformPool.Add(newEntity);
            ref var fractionComponent = ref _fractionPool.Add(newEntity);
            var createdGameObject = Object.Instantiate(_gameData.plaintData.Prefab);
            var entityHandler = createdGameObject.AddComponent<EntityHandler>();
            entityHandler.Init(newEntity);
            transformComponent.transform = createdGameObject.transform;
            fractionComponent.value = 1;
        }
    }
}