using Leopotam.EcsLite;

namespace src.Scripts.Abstraction
{
    public abstract class EcsMarkUpdater : EcsRunForeach
    {
        protected void UpdateMark<T>(EcsPool<T> ecsPool, int entity) where T : struct
        {
            var hasComponent = ecsPool.Has(entity);
            if (MarkCondition(entity))
            {
                if (!hasComponent) ecsPool.Add(entity);
            }
            else
            {
                if (hasComponent) ecsPool.Del(entity);
            }
        }
        
        protected abstract bool MarkCondition(int entity);
    }
}