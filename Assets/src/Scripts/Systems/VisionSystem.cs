using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class VisionSystem : EcsRunForeach
    {
        private EcsPool<VisionComponent> _visionPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<MemoryComponent> _memoryPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<VisionComponent>().Inc<TransformComponent>().Inc<MemoryComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _visionPool = GetPool<VisionComponent>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var visionComponent = ref _visionPool.Get(entity);
            ref var transformComponent = ref _transformPool.Get(entity);
            ref var memoryComponent = ref _memoryPool.Get(entity);
            var lineSight = visionComponent.lineSight;
            var rangeSight = visionComponent.rangeSight;
            
            for (int i = 0; i < 5; i++)
            {
                float angle = (-lineSight / 2f) + (lineSight / 4f) + ((lineSight / 4f) * i);
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transformComponent.transform.forward;
                RaycastHit2D hit = Physics2D.Raycast(transformComponent.transform.position, direction, rangeSight);
                if (hit.collider != null)
                {
                    var detectedGameObject = hit.collider.gameObject;
                    var entityHandler = detectedGameObject.GetComponent<EntityHandler>();
                    if (entityHandler != null) memoryComponent.detectedEntities.Add(entityHandler);
                }
            }
        }
    }
}