using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class PredatorHungerBehaviourSystem : EcsMarkUpdater
    {
        private readonly EcsPoolInject<SatietyComponent> _satietyPool = default;
        private readonly EcsPoolInject<HuntingMark> _huntingPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<PredatorMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter){}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            UpdateMark(_huntingPool.Value, entity);
        }

        protected override bool MarkCondition(int entity)
        {
            var hasHunting = _huntingPool.Value.Has(entity);
            ref var satietyComponent = ref _satietyPool.Value.Get(entity);
            if (hasHunting) return satietyComponent.NormalizedValue < 0.9f;
            else return satietyComponent.NormalizedValue < 0.6f;
            
        }
    }
}