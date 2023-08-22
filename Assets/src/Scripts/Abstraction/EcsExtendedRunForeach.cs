
using Leopotam.EcsLite;

namespace src.Scripts.Abstraction
{
    public abstract class EcsExtendedRunForeach : EcsInit, IEcsRunSystem
    {

        public void Run(IEcsSystems systems)
        {
            BeforeForeach(systems, _world, _filter);
            foreach (var entity in _filter)
            {
                InForeach(systems, entity, _world, _filter);
            }
            AfterForeach(systems, _world, _filter);
        }
        
        protected abstract void BeforeForeach(IEcsSystems systems, EcsWorld world, EcsFilter filter);
        protected abstract void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter);
        protected abstract void AfterForeach(IEcsSystems systems, EcsWorld world, EcsFilter filter);
    }
}