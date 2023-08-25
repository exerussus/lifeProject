using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class CreatingPlayerSystem : EcsInit
    {
        private readonly EcsPoolInject<HerbivoreMark> _herbivorePool = default;
        private readonly EcsPoolInject<HealthComponent> _creatureHealthPool = default;
        private readonly EcsPoolInject<StaminaComponent> _creatureStaminaPool = default;
        private readonly EcsPoolInject<MovePointComponent> _movePointPool = default;
        private readonly EcsPoolInject<MoveSpeedComponent> _moveSpeedPool = default;
        private readonly EcsPoolInject<MoveDirectionComponent> _moveStatePool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<SatietyComponent> _satietyPool = default;
        private readonly EcsPoolInject<FractionComponent> _fractionPool = default;
        private readonly EcsPoolInject<VisionComponent> _visionPool = default;
        private readonly EcsPoolInject<MemoryComponent> _memoryPool = default;
        private readonly EcsPoolInject<RotationSpeedComponent> _rotationSpeedPool = default;
        private readonly EcsPoolInject<PlayerMark> _playerPool = default;
        private readonly EcsPoolInject<CreatureMark> _creaturePool = default;
        
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
            var creatureCount = 2;
            for (int i = 0; i < creatureCount; i++)
            {
                Create();
            }
        }

        private void Create()
        {
            var playerEntity = _world.NewEntity();
            _movePointPool.Value.Add(playerEntity);
            _moveStatePool.Value.Add(playerEntity);
            _playerPool.Value.Add(playerEntity);
            _creaturePool.Value.Add(playerEntity);
            _herbivorePool.Value.Add(playerEntity);
            ref var memoryComponent = ref _memoryPool.Value.Add(playerEntity);
            ref var creatureHealthComponent = ref _creatureHealthPool.Value.Add(playerEntity);
            ref var creatureStaminaComponent = ref _creatureStaminaPool.Value.Add(playerEntity);
            ref var moveSpeedComponent = ref _moveSpeedPool.Value.Add(playerEntity);
            ref var transformComponent = ref _transformPool.Value.Add(playerEntity);
            ref var satietyComponent = ref _satietyPool.Value.Add(playerEntity);
            ref var fractionComponent = ref _fractionPool.Value.Add(playerEntity);
            ref var visionComponent = ref _visionPool.Value.Add(playerEntity);
            ref var rotationSpeedComponent = ref _rotationSpeedPool.Value.Add(playerEntity);

            rotationSpeedComponent.value = _gameData.playerData.RotationSpeed;
            memoryComponent.detectedEntities = new List<EntityHandler>();
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
    }   
}