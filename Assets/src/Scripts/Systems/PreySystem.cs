using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class PreySystem : EcsRunForeach
    {
        private readonly EcsPoolInject<PreyComponent> _preyPool = default;
        private readonly EcsPoolInject<HuntingMark> _huntingPool = default;
        private readonly EcsPoolInject<HarvestingMark> _harvestingPool = default;
        
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
        }
    }
}