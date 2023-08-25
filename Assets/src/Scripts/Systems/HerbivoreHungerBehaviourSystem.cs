﻿using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class HerbivoreHungerBehaviourSystem : EcsMarkUpdater
    {
        private EcsPool<SatietyComponent> _satietyPool;
        private EcsPool<HarvestingMark> _harvestingPool;
        private EcsPool<HerbivoreMark> _herbivorePool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return world.Filter<HerbivoreMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _satietyPool = _world.GetPool<SatietyComponent>();
            _harvestingPool = _world.GetPool<HarvestingMark>();
            _herbivorePool = _world.GetPool<HerbivoreMark>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            UpdateMark(_harvestingPool, entity);
        }

        protected override bool MarkCondition(int entity)
        {
            var hasHarvesting = _harvestingPool.Has(entity);
            ref var satietyComponent = ref _satietyPool.Get(entity);
            if (hasHarvesting) return satietyComponent.NormalizedValue < 0.9f;
            else return satietyComponent.NormalizedValue < 0.6f;
        }
    }
}