using Leopotam.EcsLite;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MovementCreatureBehaviourSystem : EcsRunForeach
    {
        private EcsPool<PreyComponent> _preyPool;
        private EcsPool<WorringComponent> _worringPool;
        private EcsPool<HuntingMark> _huntingPool;
        private EcsPool<HarvestingMark> _harvestingPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<MovePointComponent> _movePointPool;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<CreatureMark>().Exc<VegetationMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter)
        {
            _preyPool = _world.GetPool<PreyComponent>();
            _worringPool = _world.GetPool<WorringComponent>();
            _huntingPool = _world.GetPool<HuntingMark>();
            _harvestingPool = _world.GetPool<HarvestingMark>();
            _transformPool = _world.GetPool<TransformComponent>();
            _movePointPool = _world.GetPool<MovePointComponent>();
        }

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            var hasPrey = _preyPool.Has(entity);
            var hasWorring = _worringPool.Has(entity);
            var hasHunting = _huntingPool.Has(entity);
            var hasHarvesting = _harvestingPool.Has(entity);

            if (hasWorring) RunForestRun(entity);
            else if (hasPrey) RunToPrey(entity);
            else if (hasHunting || hasHarvesting) LookAround(entity);
            else Chill(entity);
        }


        private void RunForestRun(int entity)
        {
            ref var worringComponent = ref _worringPool.Get(entity);
            ref var movePointComponent = ref _movePointPool.Get(entity);
            ref var transformEnemyComponent = ref _transformPool.Get(worringComponent.entity);
            ref var transformCreatureComponent = ref _transformPool.Get(entity);
            
            var direction = transformCreatureComponent.transform.position - transformEnemyComponent.transform.position;
            movePointComponent.destination = -direction.normalized * 5f;
        }

        private void RunToPrey(int entity)
        {
            ref var preyComponent = ref _preyPool.Get(entity);
            ref var movePointComponent = ref _movePointPool.Get(entity);
            ref var transformPreyComponent = ref _transformPool.Get(preyComponent.entity);
            movePointComponent.destination = transformPreyComponent.transform.position;
        }

        private void LookAround(int entity)
        {
            ref var transformCreatureComponent = ref _transformPool.Get(entity);
            ref var movePointComponent = ref _movePointPool.Get(entity);

            var dir = transformCreatureComponent.transform.up * 10f;
            movePointComponent.destination = new Vector3(dir.x, dir.y + Random.Range(-1f, 1f), 0);
        }

        private void Chill(int entity)
        {
            var randomFloat = Random.Range(0, 10f);

            if (randomFloat < 7f) return;
            
            ref var transformCreatureComponent = ref _transformPool.Get(entity);
            ref var movePointComponent = ref _movePointPool.Get(entity);
            
            var dir = transformCreatureComponent.transform.up;
            movePointComponent.destination = new Vector3(dir.x, dir.y + Random.Range(-1f, 1f), 0);
        }
        
    }
}