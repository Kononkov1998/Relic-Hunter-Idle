using Data.Runtime;
using Data.Scene;
using Data.Static;
using Factories;
using Leopotam.Ecs;

namespace Systems
{
    public class SpawnTreasureDiggersSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private WorkerFactory _workerFactory;

        public void Run()
        {
            if (_runtimeData.TreasureDiggersCount < _runtimeData.MaxTreasureDiggersCount)
            {
                _workerFactory.SpawnTreasureDigger();
                _runtimeData.TreasureDiggersCount++;
            }
        }
    }
}