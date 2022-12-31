using Components;
using Leopotam.Ecs;
using Object = UnityEngine.Object;

namespace Systems
{
    public class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<NeedDestroyNextFrame> _filter;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                Object.Destroy(entity.Get<NeedDestroyNextFrame>().GameObject);
                entity.Destroy();
            }
        }
    }
}