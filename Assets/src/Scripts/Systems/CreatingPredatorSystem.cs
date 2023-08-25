using System.Collections;
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
    public class CreatingPredatorSystem : EcsInit
    {
        private readonly EcsPoolInject<PredatorMark> _predatorPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;
        private readonly EcsPoolInject<CreatureMark> _creaturePool;
        private readonly EcsPoolInject<FractionComponent> _fractionPool;
        private readonly EcsPoolInject<HealthComponent> _creatureHealthPool = default;
        private readonly EcsPoolInject<StaminaComponent> _creatureStaminaPool = default;
        private readonly EcsPoolInject<MovePointComponent> _movePointPool = default;
        private readonly EcsPoolInject<MoveSpeedComponent> _moveSpeedPool = default;
        private readonly EcsPoolInject<MoveDirectionComponent> _moveStatePool = default;
        private readonly EcsPoolInject<SatietyComponent> _satietyPool = default;
        private readonly EcsPoolInject<VisionComponent> _visionPool = default;
        private readonly EcsPoolInject<MemoryComponent> _memoryPool = default;
        private readonly EcsPoolInject<RotationSpeedComponent> _rotationSpeedPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<PredatorMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            Create();
        }
        
        private void Create()
        {
            var newEntity = _world.NewEntity();
            
            _creaturePool.Value.Add(newEntity);
            _predatorPool.Value.Add(newEntity);
            _movePointPool.Value.Add(newEntity);
            _moveStatePool.Value.Add(newEntity);
            ref var transformComponent = ref _transformPool.Value.Add(newEntity);
            ref var fractionComponent = ref _fractionPool.Value.Add(newEntity);
            ref var memoryComponent = ref _memoryPool.Value.Add(newEntity);
            ref var creatureHealthComponent = ref _creatureHealthPool.Value.Add(newEntity);
            ref var creatureStaminaComponent = ref _creatureStaminaPool.Value.Add(newEntity);
            ref var moveSpeedComponent = ref _moveSpeedPool.Value.Add(newEntity);
            ref var satietyComponent = ref _satietyPool.Value.Add(newEntity);
            ref var visionComponent = ref _visionPool.Value.Add(newEntity);
            ref var rotationSpeedComponent = ref _rotationSpeedPool.Value.Add(newEntity);

            var createdGameObject = Object.Instantiate(_gameData.predatorData.Prefab);
            var entityHandler = createdGameObject.AddComponent<EntityHandler>();
            entityHandler.Init(newEntity);
            
            rotationSpeedComponent.value = _gameData.predatorData.RotationSpeed;
            memoryComponent.detectedEntities = new List<EntityHandler>();
            creatureHealthComponent.MaxValue = _gameData.predatorData.MaxHealth;
            creatureStaminaComponent.MaxValue = _gameData.predatorData.MaxStamina;
            satietyComponent.MaxValue = _gameData.predatorData.MaxSatiety;
            moveSpeedComponent.value = _gameData.predatorData.MoveSpeed;
            fractionComponent.value = _gameData.predatorData.Fraction;
            visionComponent.rangeSight = _gameData.predatorData.RangeOfSight;
            visionComponent.lineSight = _gameData.predatorData.LineOfSight;
            transformComponent.transform = createdGameObject.transform;
        }
    }
}