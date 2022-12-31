using Components;
using Components.Events;
using Components.Timers;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class FindTreasureSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private EcsFilter<TreasureHunter, TimerDoneEvent<FindTreasureTimer>>.Exclude<SpawnedThisFrameEvent>
            _doneStayingHunters;

        public void Run()
        {
            foreach (int index in _doneStayingHunters)
            {
                EcsEntity entity = _doneStayingHunters.GetEntity(index);
                float random = Random.Range(0f, 100f);
                if (random <= _configuration.TreasureHunterData.ChanceToFindTreasure)
                    SpawnFlag(entity.Get<TransformProvider>().Value.position);
            }
        }

        private void SpawnFlag(Vector3 position)
        {
            position.y = _configuration.FlagPrefab.transform.position.y;
            GameObject flagView = Object.Instantiate(_configuration.FlagPrefab, position,
                Quaternion.identity, _sceneData.FlagParent);

            EcsEntity entity = _ecsWorld.NewEntity();
            entity.Replace(new TransformProvider {Value = flagView.transform});
            entity.Get<Flag>();
        }
    }
}