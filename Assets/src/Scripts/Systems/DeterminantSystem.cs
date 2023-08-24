using System.Collections.Generic;
using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using src.Scripts.MonoBehaviours;
using src.Scripts.TalentMarks;

namespace src.Scripts.Systems
{
    public class DeterminantSystem : EcsRunForeach
    {
        private EcsPool<MemoryComponent> _memoryPool;
        private EcsPool<WorringComponent> _worringPool;
        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<PreyComponent> _preyPool;
        private EcsPool<HerbivoreMark> _herbivorePool;
        private EcsPool<VegetationMark> _vegetationPool;
        private EcsPool<FractionComponent> _fractionPool;
        private EcsPool<InsightTalent> _insightTalentPool;
        private EcsPool<PredatorMark> _predatorPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<MemoryComponent>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _memoryPool = _world.GetPool<MemoryComponent>();
            _worringPool = _world.GetPool<WorringComponent>();
            _healthPool = _world.GetPool<HealthComponent>();
            _preyPool = _world.GetPool<PreyComponent>(); 
            _herbivorePool = _world.GetPool<HerbivoreMark>(); 
            _vegetationPool = _world.GetPool<VegetationMark>(); 
            _fractionPool = _world.GetPool<FractionComponent>();
            _insightTalentPool = _world.GetPool<InsightTalent>();
            _predatorPool = _world.GetPool<PredatorMark>();
        }
        
        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            ref var memoryComponent = ref _memoryPool.Get(entity);
            
            foreach (var entityHandler in memoryComponent.detectedEntities)
            {
                var enemyEntity = entityHandler.ID;

                var isSameFraction = _fractionPool.Get(entity).value == _fractionPool.Get(enemyEntity).value;
                
                if (isSameFraction) continue;
                
                ref var healthCreatureComponent = ref _healthPool.Get(entity);
                ref var healthEnemyComponent = ref _healthPool.Get(enemyEntity);
                var hasPredatorEnemy = _predatorPool.Has(enemyEntity);
                var hasPredatorCreature = _predatorPool.Has(entity);
                var hasVegetationEnemy = _vegetationPool.Has(enemyEntity);
                var hasHerbivoreCreature = _herbivorePool.Has(entity);
                var hasHerbivoreEnemy = _herbivorePool.Has(enemyEntity);
                
                if (hasPredatorCreature 
                    && hasPredatorEnemy 
                    && healthCreatureComponent.Value > healthEnemyComponent.Value 
                    && !hasVegetationEnemy ||
                    hasPredatorCreature && hasHerbivoreEnemy && !hasVegetationEnemy)
                {
                    MakeItPrey(entity, enemyEntity);
                }
                else if (!hasPredatorCreature && hasPredatorEnemy && !hasVegetationEnemy || 
                         !_insightTalentPool.Has(entity) 
                         && hasHerbivoreEnemy
                         && healthCreatureComponent.Value < healthEnemyComponent.Value
                         && !hasVegetationEnemy)
                {
                    MakeItWorring(entity, enemyEntity);
                }
                else if (hasHerbivoreCreature && hasVegetationEnemy) MakeItPrey(entity, enemyEntity);
            }
            
            memoryComponent.detectedEntities = new List<EntityHandler>();
        }

        private void MakeItWorring(int creatureEntity, int enemyEntity)
        {
            var hasWorringComponent = _worringPool.Has(creatureEntity);
            var hasPreyComponent = _preyPool.Has(creatureEntity);
            if(hasPreyComponent) _preyPool.Del(creatureEntity);
            
            if (hasWorringComponent)
            {
                ref var worringComponent = ref _worringPool.Get(creatureEntity);
                worringComponent.entity = enemyEntity;
            }
            else
            {
                ref var worringComponent = ref _worringPool.Add(creatureEntity);
                worringComponent.entity = enemyEntity;
            }
        }
        
        private void MakeItPrey(int creatureEntity, int enemyEntity)
        {
            var hasPreyComponent = _preyPool.Has(creatureEntity);
            var hasWorringComponent = _worringPool.Has(creatureEntity);
            if (hasPreyComponent || hasWorringComponent) return;
            
            ref var preyComponent = ref _preyPool.Add(creatureEntity);
            preyComponent.entity = enemyEntity;
        }
    }
}

// if (hasPredatorEnemy)
// {
//     if (hasPredatorCreature)
//     {
//         if (healthCreatureComponent.Value > healthEnemyComponent.Value)
//         {
//             MakeItPrey(entity, enemyEntity);
//         }
//         else
//         {
//             MakeItWorring(entity, enemyEntity);
//         }
//     }
//     else
//     {
//         MakeItWorring(entity, enemyEntity);
//     }
// }
// else
// {
//     if (hasPredatorCreature)
//     {
//         MakeItPrey(entity, enemyEntity);
//     }
//     else
//     {
//         if (healthCreatureComponent.Value < healthEnemyComponent.Value)
//         {
//             MakeItWorring(entity, enemyEntity);
//         }
//     }
// }