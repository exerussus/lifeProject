using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class HerbivoreHungerBehaviourSystem : EcsMarkUpdater
    {
        private readonly EcsPoolInject<SatietyComponent> _satietyPool = default;
        private readonly EcsPoolInject<HarvestingMark> _harvestingPool = default;
        private readonly EcsPoolInject<HerbivoreMark> _herbivorePool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<HerbivoreMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            UpdateMark(_harvestingPool.Value, entity);
        }

        protected override bool MarkCondition(int entity)
        {
            var hasHarvesting = _harvestingPool.Value.Has(entity);
            ref var satietyComponent = ref _satietyPool.Value.Get(entity);
            if (hasHarvesting) return satietyComponent.NormalizedValue < 0.9f;
            else return satietyComponent.NormalizedValue < 0.6f;
        }
    }
}