using Leopotam.EcsLite;

namespace src.Scripts.Abstraction
{
    public abstract class EcsMarkUpdater : EcsRunForeach
    {
        protected void UpdateMark<T>(EcsPool<T> ecsPool, int entity) where T : struct
        {
            var harvestingComponent = ecsPool.Has(entity);
            if (MarkCondition(entity))
            {
                if (!harvestingComponent) ecsPool.Add(entity);
            }
            else
            {
                if (harvestingComponent) ecsPool.Del(entity);
            }
        }
        
        protected abstract bool MarkCondition(int entity);
    }
}