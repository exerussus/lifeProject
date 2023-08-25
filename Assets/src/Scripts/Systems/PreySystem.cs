using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class PreySystem : EcsRunForeach
    {
        private EcsPool<PreyComponent> _preyPool;
        private EcsPool<HuntingMark> _huntingPool;
        private EcsPool<HarvestingMark> _harvestingPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<PreyComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _preyPool = _world.GetPool<PreyComponent>();
            _huntingPool = _world.GetPool<HuntingMark>();
            _harvestingPool = _world.GetPool<HarvestingMark>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            var hasHarvesting = _harvestingPool.Has(entity);
            var hasHunting = _huntingPool.Has(entity);
            
            if (!hasHarvesting && !hasHunting)
            {
                _preyPool.Del(entity);
                return;
            }
            ref var prey = ref _preyPool.Get(entity);
        }
    }
}