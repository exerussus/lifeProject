using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Logic;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class CreatingPlayerSystem : EcsInit
    {
        private EcsPool<CreatureTypeComponent> _creatureTypePool;
        private EcsPool<CreatureResourceComponent> _creatureResourcePool;
        private EcsPool<MovePointComponent> _movePointPool;
        private EcsPool<MoveSpeedComponent> _moveSpeedPool;
        private EcsPool<MoveStateComponent> _moveStatePool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<SatietyComponent> _satietyPool;
        private EcsPool<FractionComponent> _fractionPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<CreatureTypeComponent>().
                Inc<CreatureResourceComponent>().
                Inc<MovePointComponent>().
                Inc<MoveSpeedComponent>().
                Inc<MoveStateComponent>().
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
            ref var creatureTypeComponent = ref _creatureTypePool.Add(playerEntity);
            ref var creatureResourceComponent = ref _creatureResourcePool.Add(playerEntity);
            ref var moveSpeedComponent = ref _moveSpeedPool.Add(playerEntity);
            ref var transformComponent = ref _transformPool.Add(playerEntity);
            ref var satietyComponent = ref _satietyPool.Add(playerEntity);
            ref var fractionComponent = ref _fractionPool.Add(playerEntity);

            creatureTypeComponent.creatureType = _gameData.playerData.CreatureType;
            creatureResourceComponent.health = new CreatureResource(_gameData.playerData.MaxHealth);
            creatureResourceComponent.stamina = new CreatureResource(_gameData.playerData.MaxStamina);
            satietyComponent.satiety = new CreatureResource(_gameData.playerData.MaxSatiety);
            moveSpeedComponent.value = _gameData.playerData.MoveSpeed;
            fractionComponent.value = _gameData.playerData.Fraction;
            
            var createdGameObject = Object.Instantiate(_gameData.playerData.Prefab);

            transformComponent.transform = createdGameObject.transform;
        }
        
        private void PreparePools()
        {
            _creatureTypePool = _world.GetPool<CreatureTypeComponent>();
            _creatureResourcePool = _world.GetPool<CreatureResourceComponent>();
            _movePointPool = _world.GetPool<MovePointComponent>();
            _moveSpeedPool = _world.GetPool<MoveSpeedComponent>();
            _moveStatePool = _world.GetPool<MoveStateComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _satietyPool = _world.GetPool<SatietyComponent>();
            _fractionPool = _world.GetPool<FractionComponent>();
        }
    }   
}