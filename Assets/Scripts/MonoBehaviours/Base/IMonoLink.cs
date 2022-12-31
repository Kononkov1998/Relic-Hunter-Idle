using Leopotam.Ecs;

namespace MonoBehaviours.Base
{
    public interface IMonoLink
    {
        public void Make(ref EcsEntity entity);
    }
}