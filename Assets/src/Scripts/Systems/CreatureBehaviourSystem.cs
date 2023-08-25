using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;

namespace src.Scripts.Systems
{
    public class CreatureBehaviourSystem : EcsRunForeach
    {
        private readonly EcsPoolInject<HarvestingMark> _harvestingPool = default;
        private readonly EcsPoolInject<HuntingMark> _huntingPool = default;
        private readonly EcsPoolInject<WorringComponent> _worringPool = default;
        private readonly EcsPoolInject<MemoryComponent> _memoryPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<CreatureMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            var hasHarvesting = _harvestingPool.Value.Has(entity);
            var hasHunting = _huntingPool.Value.Has(entity);
            var hasWorring = _worringPool.Value.Has(entity);

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