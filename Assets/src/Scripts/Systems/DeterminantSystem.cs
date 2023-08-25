using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using src.Scripts.TalentMarks;

namespace src.Scripts.Systems
{
    public class DeterminantSystem : EcsRunForeach
    {
        private EcsPoolInject<MemoryComponent> _memoryPool = default;
        private EcsPoolInject<WorringComponent> _worringPool = default;
        private EcsPoolInject<HealthComponent> _healthPool = default;
        private EcsPoolInject<PreyComponent> _preyPool = default;
        private EcsPoolInject<HerbivoreMark> _herbivorePool = default;
        private EcsPoolInject<VegetationMark> _vegetationPool = default;
        private EcsPoolInject<FractionComponent> _fractionPool = default;
        private EcsPoolInject<InsightTalent> _insightTalentPool = default;
        private EcsPoolInject<PredatorMark> _predatorPool = default;
        private EcsPoolInject<HarvestingMark> _harvestingPool = default;
        private EcsPoolInject<HuntingMark> _huntingPool = default;
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<MemoryComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}
        
        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var memoryComponent = ref _memoryPool.Value.Get(entity);
            
            foreach (var entityHandler in memoryComponent.detectedEntities)
            {
                var enemyEntity = entityHandler.ID;

                var isSameFraction = _fractionPool.Value.Get(entity).value == _fractionPool.Value.Get(enemyEntity).value;
                var hasVegetationEnemy = _vegetationPool.Value.Has(enemyEntity);
                var hasHerbivoreCreature = _herbivorePool.Value.Has(entity);
                var hasHarvesting = _harvestingPool.Value.Has(entity);
                
                if (hasHerbivoreCreature && hasHarvesting && hasVegetationEnemy)
                {
                    MakeItPrey(entity, enemyEntity);
                    continue;
                }
                if (isSameFraction || hasVegetationEnemy) continue;
                
                ref var healthCreatureComponent = ref _healthPool.Value.Get(entity);
                ref var healthEnemyComponent = ref _healthPool.Value.Get(enemyEntity);
                var hasPredatorEnemy = _predatorPool.Value.Has(enemyEntity);
                var hasPredatorCreature = _predatorPool.Value.Has(entity);
                var hasHerbivoreEnemy = _herbivorePool.Value.Has(enemyEntity);
                var hasHunting = _huntingPool.Value.Has(entity);
                
                if (hasPredatorCreature 
                    && hasPredatorEnemy 
                    && healthCreatureComponent.Value > healthEnemyComponent.Value 
                    && hasHunting ||
                    hasPredatorCreature && hasHerbivoreEnemy && hasHunting)
                {
                    MakeItPrey(entity, enemyEntity);
                }
                else if (!hasPredatorCreature && hasPredatorEnemy || 
                         !_insightTalentPool.Value.Has(entity) 
                         && hasHerbivoreEnemy
                         && healthCreatureComponent.Value < healthEnemyComponent.Value)
                {
                    MakeItWorring(entity, enemyEntity);
                }
            }
            
            memoryComponent.detectedEntities = new List<EntityHandler>();
        }

        private void MakeItWorring(int creatureEntity, int enemyEntity)
        {
            var hasWorringComponent = _worringPool.Value.Has(creatureEntity);
            var hasPreyComponent = _preyPool.Value.Has(creatureEntity);
            if(hasPreyComponent) _preyPool.Value.Del(creatureEntity);
            
            if (hasWorringComponent)
            {
                ref var worringComponent = ref _worringPool.Value.Get(creatureEntity);
                worringComponent.entity = enemyEntity;
            }
            else
            {
                ref var worringComponent = ref _worringPool.Value.Add(creatureEntity);
                worringComponent.entity = enemyEntity;
            }
        }
        
        private void MakeItPrey(int creatureEntity, int enemyEntity)
        {
            var hasPreyComponent = _preyPool.Value.Has(creatureEntity);
            var hasWorringComponent = _worringPool.Value.Has(creatureEntity);
            if (hasPreyComponent || hasWorringComponent) return;
            
            ref var preyComponent = ref _preyPool.Value.Add(creatureEntity);
            preyComponent.entity = enemyEntity;
        }
    }
}