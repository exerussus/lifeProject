using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class PredatorHungerBehaviourSystem : EcsMarkUpdater
    {
        private EcsPool<SatietyComponent> _satietyPool;
        private EcsPool<PredatorMark> _predatorPool;
        private EcsPool<HuntingMark> _huntingPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<PredatorMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _satietyPool = _world.GetPool<SatietyComponent>();
            _predatorPool = _world.GetPool<PredatorMark>();
            _huntingPool = _world.GetPool<HuntingMark>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            UpdateMark(_huntingPool, entity);
        }

        protected override bool MarkCondition(int entity)
        {
            var hasHunting = _huntingPool.Has(entity);
            ref var satietyComponent = ref _satietyPool.Get(entity);
            if (hasHunting) return satietyComponent.NormalizedValue < 0.9f;
            else return satietyComponent.NormalizedValue < 0.6f;
            
        }
    }
}