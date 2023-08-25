using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MoveToMouseSystem : EcsRunForeach
    {
        private EcsPool<MovePointComponent> _movePointPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<PlayerMark>().Inc<MovePointComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _movePointPool = _world.GetPool<MovePointComponent>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var movePointComponent = ref _movePointPool.Get(entity);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var destination = mousePosition;
            movePointComponent.destination = destination;
        }
    }
}