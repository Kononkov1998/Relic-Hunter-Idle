using Data.Runtime;
using Data.Scene;
using Leopotam.Ecs;

namespace Systems
{
    public class RuntimeDataViewSystem : IEcsInitSystem
    {
        private SceneData _sceneData;
        private RuntimeData _runtimeData;

        public void Init()
        {
            _sceneData.RuntimeDataView.Data=_runtimeData;
        }
    }
}