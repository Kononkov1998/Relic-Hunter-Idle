using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using Services.SaveLoad;

namespace Systems
{
    public class RuntimeDataLoadSystem : IEcsInitSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private ISaveLoadService _saveLoadService;

        public void Init()
        {
            if (_saveLoadService.Exists(_runtimeData.SaveId))
                _runtimeData = _saveLoadService.Load<RuntimeData>(_runtimeData.SaveId);
            else
                FillDefaultValues();
        }

        private void FillDefaultValues()
        {
            _runtimeData.MaxTreasureHuntersCount = _configuration.TreasureHunterData.MaxCount;
            _runtimeData.MaxTreasureCollectorsCount = _configuration.TreasureCollectorData.MaxCount;
            _runtimeData.MaxTreasureDiggersCount = _configuration.TreasureDiggerData.MaxCount;
        }
    }
}