﻿using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.MonoBehaviours;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class VisionSystem : EcsRunForeach
    {
        private readonly EcsPoolInject<VisionComponent> _visionPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<MemoryComponent> _memoryPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<VisionComponent>().Inc<TransformComponent>().Inc<MemoryComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var visionComponent = ref _visionPool.Value.Get(entity);
            ref var transformComponent = ref _transformPool.Value.Get(entity);
            ref var memoryComponent = ref _memoryPool.Value.Get(entity);
            var lineSight = visionComponent.lineSight;
            var rangeSight = visionComponent.rangeSight;
            const int rayCount = 5;

            float j = 0;

            for (int i = 0; i < rayCount; i++)
            {
                var x = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += (lineSight / rayCount) * Mathf.Deg2Rad;

                Vector2 direction = transformComponent.transform.TransformDirection(new Vector2(x, y));
                CastRay(direction, transformComponent.transform.position, rangeSight, memoryComponent);

                if (x != 0)
                {
                    direction = transformComponent.transform.TransformDirection(new Vector3(-x, y, 0));
                    CastRay(direction, transformComponent.transform.position, rangeSight, memoryComponent);
                }
            }
        }

        private void CastRay(Vector2 direction, Vector3 position, float rangeSight, MemoryComponent memoryComponent)
        {

            position = new Vector3(position.x + direction.x/2, position.y + direction.y/2, 0);
            RaycastHit2D hit = Physics2D.Raycast(position, direction, rangeSight);

            if(hit.collider != null)
            {
                var detectedGameObject = hit.collider.gameObject;
                var entityHandler = detectedGameObject.GetComponent<EntityHandler>();
                if (entityHandler != null) memoryComponent.detectedEntities.Add(entityHandler);
                
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawRay(position, direction * rangeSight, Color.red);
            }
        }
    }
}