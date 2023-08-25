using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MoveToMouseSystem : EcsRunForeach
    {
        private readonly EcsPoolInject<MovePointComponent> _movePointPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<PlayerMark>().Inc<MovePointComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter){}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var destination = mousePosition;
            movePointComponent.destination = destination;
        }
    }
}