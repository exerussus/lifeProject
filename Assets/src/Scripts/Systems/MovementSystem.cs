
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MovementSystem : EcsRunForeach
    {
        private readonly EcsPoolInject<MovePointComponent> _movePointPool = default;
        private readonly EcsPoolInject<MoveSpeedComponent> _speedPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<RotationSpeedComponent> _rotationSpeedPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<MoveDirectionComponent>().
                Inc<MoveSpeedComponent>().
                Inc<TransformComponent>().
                Exc<ImmovableMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);
            ref var transformComponent = ref _transformPool.Value.Get(entity);
            ref var moveSpeedComponent = ref _speedPool.Value.Get(entity);
            ref var rotationSpeedComponent = ref _rotationSpeedPool.Value.Get(entity);
            var transform = transformComponent.transform;
            
            var creaturePosition = transform.position;
            var point = movePointComponent.destination;
            float angle = Mathf.Atan2(point.y - creaturePosition.y, point.x - creaturePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), rotationSpeedComponent.value * Time.deltaTime);
            
            var distance = Vector3.Distance(creaturePosition, point);
            var correction = 10f;
            var minDistance = 0.01f;
            // if (distance - correction > minDistance)
            // {
            //     var offset =
            //         transformComponent.transform.up * (moveSpeedComponent.value * Time.fixedDeltaTime);
            //
            //     transformComponent.transform.position += offset;
            // }
            var offset =
                transformComponent.transform.up * (moveSpeedComponent.value * Time.fixedDeltaTime);
            
            transformComponent.transform.position += offset;
        }
    }
}