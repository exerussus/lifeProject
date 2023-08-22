
using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MovementSystem : EcsRunForeach
    {
        private EcsPool<MoveStateComponent> _statePool;
        private EcsPool<MoveSpeedComponent> _speedPool;
        private EcsPool<TransformComponent> _transformPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<MoveStateComponent>().Inc<MoveSpeedComponent>().Inc<TransformComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)

        {
            _statePool = GetPool<MoveStateComponent>();
            _speedPool = GetPool<MoveSpeedComponent>();
            _transformPool = GetPool<TransformComponent>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var stateComponent = ref _statePool.Get(entity);
            if (!stateComponent.required) return;

            ref var transformComponent = ref _transformPool.Get(entity);
            ref var moveSpeedComponent = ref _speedPool.Get(entity);

            var offset =
                stateComponent.direction * (moveSpeedComponent.value * Time.fixedDeltaTime);

            transformComponent.transform.position = offset;
        }
    }
}