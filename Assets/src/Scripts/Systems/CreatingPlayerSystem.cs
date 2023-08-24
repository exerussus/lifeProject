using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class CreatingPlayerSystem : EcsInit
    {
        private EcsPool<HerbivoreMark> _herbivorePool;
        private EcsPool<HealthComponent> _creatureHealthPool;
        private EcsPool<StaminaComponent> _creatureStaminaPool;
        private EcsPool<MovePointComponent> _movePointPool;
        private EcsPool<MoveSpeedComponent> _moveSpeedPool;
        private EcsPool<MoveDirectionComponent> _moveStatePool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<SatietyComponent> _satietyPool;
        private EcsPool<FractionComponent> _fractionPool;
        private EcsPool<VisionComponent> _visionPool;
        private EcsPool<MemoryComponent> _memoryPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<HerbivoreMark>().
                Inc<HealthComponent>().
                Inc<MovePointComponent>().
                Inc<MoveSpeedComponent>().
                Inc<MoveDirectionComponent>().
                Inc<TransformComponent>().
                Inc<SatietyComponent>().
                Inc<FractionComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            PreparePools();
            
            var playerEntity = _world.NewEntity();
            _movePointPool.Add(playerEntity);
            _moveStatePool.Add(playerEntity);
            _memoryPool.Add(playerEntity);
            _herbivorePool.Add(playerEntity);
            ref var creatureHealthComponent = ref _creatureHealthPool.Add(playerEntity);
            ref var creatureStaminaComponent = ref _creatureStaminaPool.Add(playerEntity);
            ref var moveSpeedComponent = ref _moveSpeedPool.Add(playerEntity);
            ref var transformComponent = ref _transformPool.Add(playerEntity);
            ref var satietyComponent = ref _satietyPool.Add(playerEntity);
            ref var fractionComponent = ref _fractionPool.Add(playerEntity);
            ref var visionComponent = ref _visionPool.Add(playerEntity);
            
            creatureHealthComponent.MaxValue = _gameData.playerData.MaxHealth;
            creatureStaminaComponent.MaxValue = _gameData.playerData.MaxStamina;
            satietyComponent.MaxValue = _gameData.playerData.MaxSatiety;
            moveSpeedComponent.value = _gameData.playerData.MoveSpeed;
            fractionComponent.value = _gameData.playerData.Fraction;
            visionComponent.rangeSight = _gameData.playerData.RangeOfSight;
            visionComponent.lineSight = _gameData.playerData.LineOfSight;
            
            var createdGameObject = Object.Instantiate(_gameData.playerData.Prefab);
            var entityHandler = createdGameObject.AddComponent<EntityHandler>();
            
            entityHandler.Init(playerEntity);
            transformComponent.transform = createdGameObject.transform;
        }
        
        private void PreparePools()
        {
            _herbivorePool = _world.GetPool<HerbivoreMark>();
            _creatureHealthPool = _world.GetPool<HealthComponent>();
            _creatureStaminaPool = _world.GetPool<StaminaComponent>();
            _movePointPool = _world.GetPool<MovePointComponent>();
            _moveSpeedPool = _world.GetPool<MoveSpeedComponent>();
            _moveStatePool = _world.GetPool<MoveDirectionComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _satietyPool = _world.GetPool<SatietyComponent>();
            _fractionPool = _world.GetPool<FractionComponent>();
            _visionPool = _world.GetPool<VisionComponent>();
            _memoryPool = _world.GetPool<MemoryComponent>();
        }
    }   
}