using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class CreatureBehaviourSystem : EcsRunForeach
    {
        private EcsPool<HarvestingMark> _harvestingPool;
        private EcsPool<HuntingMark> _huntingPool;
        private EcsPool<WorringComponent> _worringPool;
        private EcsPool<MemoryComponent> _memoryPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<CreatureMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _harvestingPool = _world.GetPool<HarvestingMark>();
            _huntingPool = _world.GetPool<HuntingMark>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            var hasHarvesting = _harvestingPool.Has(entity);
            var hasHunting = _huntingPool.Has(entity);
            var hasWorring = _worringPool.Has(entity);

            if (hasWorring) Escape();
            else
            {
                
            }

        }

        private void LookAround()
        {
            
        }
        
        private void SeekMeet()
        {
            
        }
        
        private void SeekVegetation()
        {
            
        }
        
        private void Escape()
        {
            
        }
    }
}