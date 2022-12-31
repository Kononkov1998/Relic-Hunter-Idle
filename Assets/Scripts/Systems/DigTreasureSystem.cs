using Components;
using Components.Events;
using Components.Timers;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

public class DigTreasureSystem : IEcsRunSystem
{
    private Configuration _configuration;
    private SceneData _sceneData;
    private RuntimeData _runtimeData;
    private EcsWorld _ecsWorld;

    private EcsFilter<TreasureDigger, FlagOwner> _diggers;

    public void Run()
    {
        foreach (int collectorIndex in _diggers)
        {
            EcsEntity diggerEntity = _diggers.GetEntity(collectorIndex);
            EcsEntity flagEntity = diggerEntity.Get<FlagOwner>().Flag;
            Vector3 diggerPosition = diggerEntity.Get<TransformProvider>().Value.position;
            Transform flagTransform = flagEntity.Get<TransformProvider>().Value;
            Vector3 flagPosition = flagTransform.position;
            flagPosition.y = diggerPosition.y;

            if (Vector3.SqrMagnitude(flagPosition - diggerPosition) <=
                _configuration.TreasureDiggerData.SqrDigTreasureRange)
            {
                diggerEntity.Get<NavMeshAgentProvider>().Value.ResetPath();
                Object.Destroy(flagTransform.gameObject);
                SpawnTreasure(flagPosition);
                flagEntity.Destroy();
                diggerEntity.Del<FlagOwner>();
            }
        }
    }

    private void SpawnTreasure(Vector3 position)
    {
        position.y = _configuration.TreasurePrefab.transform.position.y;
        GameObject treasureView = Object.Instantiate(_configuration.TreasurePrefab, position,
            Quaternion.identity, _sceneData.TreasureParent);

        EcsEntity entity = _ecsWorld.NewEntity();
        entity.Replace(new TransformProvider {Value = treasureView.transform});
        entity.Get<Treasure>();
    }
}