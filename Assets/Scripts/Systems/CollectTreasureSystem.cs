using Components;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class CollectTreasureSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private EcsFilter<TreasureCollector, TreasureOwner> _collectors;

        public void Run()
        {
            foreach (int collectorIndex in _collectors)
            {
                EcsEntity collectorEntity = _collectors.GetEntity(collectorIndex);
                EcsEntity treasureEntity = collectorEntity.Get<TreasureOwner>().Treasure;
                Vector3 collectorPosition = collectorEntity.Get<TransformProvider>().Value.position;
                Transform treasureTransform = treasureEntity.Get<TransformProvider>().Value;
                Vector3 treasurePosition = treasureTransform.position;
                treasurePosition.y = collectorPosition.y;
                
                if (Vector3.SqrMagnitude(treasurePosition - collectorPosition) <=
                    _configuration.TreasureCollectorData.SqrPickUpTreasureRange)
                {
                    collectorEntity.Get<NavMeshAgentProvider>().Value.ResetPath();
                    _runtimeData.CollectedTreasures++;
                    Object.Destroy(treasureTransform.gameObject);
                    treasureEntity.Destroy();
                    collectorEntity.Del<TreasureOwner>();
                }
            }
        }
    }
}