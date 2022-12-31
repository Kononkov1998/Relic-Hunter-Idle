using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using MonoBehaviours;

namespace Systems
{
    public class UpdateUISystemTest : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;
        private UI _ui;

        public void Run()
        {
            _ui.SetTreasures(_runtimeData.CollectedTreasures);
        }
    }
}