
using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MovementSystem : EcsRunForeach
    {
        private EcsPool<MoveDirectionComponent> _moveDirectionPool;
        private EcsPool<MoveSpeedComponent> _speedPool;
        private EcsPool<TransformComponent> _transformPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<MoveDirectionComponent>().
                Inc<MoveSpeedComponent>().
                Inc<TransformComponent>().
                Exc<ImmovableMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _moveDirectionPool = GetPool<MoveDirectionComponent>();
            _speedPool = GetPool<MoveSpeedComponent>();
            _transformPool = GetPool<TransformComponent>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var moveDirectionComponent = ref _moveDirectionPool.Get(entity);
            ref var transformComponent = ref _transformPool.Get(entity);
            ref var moveSpeedComponent = ref _speedPool.Get(entity);

            var offset =
                moveDirectionComponent.direction * (moveSpeedComponent.value * Time.fixedDeltaTime);

            transformComponent.transform.position = offset;
        }
    }
}