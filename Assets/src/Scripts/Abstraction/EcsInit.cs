
using Leopotam.EcsLite;
using src.Scripts.Data;

namespace src.Scripts.Abstraction
{
    public abstract class EcsInit : IEcsInitSystem
    {
        protected EcsWorld _world;
        protected EcsFilter _filter;
        protected GameData _gameData;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = GetFilter(systems, _world);
            _gameData = systems.GetShared<GameData>();
            Initialization(systems, _world, _filter);
        }

        protected EcsPool<T> GetPool<T>() where T : struct
        {
            return _world.GetPool<T>();
        }

        protected abstract EcsFilter GetFilter(IEcsSystems systems, EcsWorld world);
        protected abstract void Initialization(IEcsSystems systems, EcsWorld world, EcsFilter filter);
    }
}