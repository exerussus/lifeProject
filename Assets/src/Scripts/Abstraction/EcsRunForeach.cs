
using Leopotam.EcsLite;

namespace src.Scripts.Abstraction
{
    public abstract class EcsRunForeach : EcsInit, IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                InForeach(systems, entity, _world, _filter);
            }
        }
        
        protected abstract void InForeach(IEcsSystems systems, int entity, EcsWorld world, EcsFilter filter);
    }
}
