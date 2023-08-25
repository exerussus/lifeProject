using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class PreySystem : EcsRunForeach
    {
        private readonly EcsPoolInject<PreyComponent> _preyPool = default;
        private readonly EcsPoolInject<HuntingMark> _huntingPool = default;
        private readonly EcsPoolInject<HarvestingMark> _harvestingPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<PreyComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            
            var hasHarvesting = _harvestingPool.Value.Has(entity);
            var hasHunting = _huntingPool.Value.Has(entity);
            
            if (!hasHarvesting && !hasHunting)
            {
                _preyPool.Value.Del(entity);
                return;
            }
            ref var prey = ref _preyPool.Value.Get(entity);

            ref var transformComponent = ref _transformPool.Value.Get(prey.entity);
            if (transformComponent.transform == null)
            {
                _preyPool.Value.Del(entity);
                return;
            }
            prey.huntingTime -= Time.fixedDeltaTime;
            if (prey.huntingTime <= 0) _preyPool.Value.Del(entity);
        }
    }
}