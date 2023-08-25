using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using src.Scripts.Abstraction;
using src.Scripts.Components;
using src.Scripts.Marks;
using UnityEngine;

namespace src.Scripts.Systems
{
    public class MovementCreatureBehaviourSystem : EcsRunForeach
    {
        private readonly EcsPoolInject<PreyComponent> _preyPool = default;
        private readonly EcsPoolInject<WorringComponent> _worringPool = default;
        private readonly EcsPoolInject<HuntingMark> _huntingPool = default;
        private readonly EcsPoolInject<HarvestingMark> _harvestingPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<MovePointComponent> _movePointPool = default;
        
        protected override EcsFilter GetFilter(IEcsSystems systems, EcsWorld world)
        {
            return _world.Filter<CreatureMark>().Exc<VegetationMark>().End();
        }

        protected override void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter) {}

        protected override void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter)
        {
            var hasPrey = _preyPool.Value.Has(entity);
            var hasWorring = _worringPool.Value.Has(entity);
            var hasHunting = _huntingPool.Value.Has(entity);
            var hasHarvesting = _harvestingPool.Value.Has(entity);

            if (hasWorring) RunForestRun(entity);
            else if (hasPrey) RunToPrey(entity);
            else if (hasHunting || hasHarvesting) LookAround(entity);
            else Chill(entity);
        }


        private void RunForestRun(int entity)
        {
            ref var worringComponent = ref _worringPool.Value.Get(entity);
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);
            ref var transformEnemyComponent = ref _transformPool.Value.Get(worringComponent.entity);
            ref var transformCreatureComponent = ref _transformPool.Value.Get(entity);
            
            var direction = transformCreatureComponent.transform.position - transformEnemyComponent.transform.position;
            movePointComponent.destination = -direction.normalized * 5f;
        }

        private void RunToPrey(int entity)
        {
            ref var preyComponent = ref _preyPool.Value.Get(entity);
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);
            ref var transformPreyComponent = ref _transformPool.Value.Get(preyComponent.entity);
            movePointComponent.destination = transformPreyComponent.transform.position;
        }

        private void LookAround(int entity)
        {
            ref var transformCreatureComponent = ref _transformPool.Value.Get(entity);
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);

            var dir = transformCreatureComponent.transform.up * 10f;
            movePointComponent.destination = new Vector3(dir.x, dir.y + Random.Range(-1f, 1f), 0);
        }

        private void Chill(int entity)
        {
            var randomFloat = Random.Range(0, 10f);

            if (randomFloat < 7f) return;
            
            ref var transformCreatureComponent = ref _transformPool.Value.Get(entity);
            ref var movePointComponent = ref _movePointPool.Value.Get(entity);
            
            var dir = transformCreatureComponent.transform.up;
            movePointComponent.destination = new Vector3(dir.x, dir.y + Random.Range(-1f, 1f), 0);
        }
        
    }
}