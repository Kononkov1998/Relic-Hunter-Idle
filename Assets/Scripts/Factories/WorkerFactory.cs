using Components;
using Components.Events;
using Components.Timers;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using MonoBehaviours.View;
using UnityEngine;

namespace Factories
{
    public class WorkerFactory
    {
        private readonly Configuration _configuration;
        private readonly SceneData _sceneData;
        private readonly EcsWorld _ecsWorld;

        public WorkerFactory(Configuration configuration, SceneData sceneData, EcsWorld ecsWorld)
        {
            _configuration = configuration;
            _sceneData = sceneData;
            _ecsWorld = ecsWorld;
        }

        public EcsEntity SpawnTreasureHunter()
        {
            IWorkerData data = _configuration.TreasureHunterData;
            Transform spawnPoint = _sceneData.TreasureHunterSpawnPoint;

            EcsEntity entity = SpawnWorker(data, spawnPoint);

            entity.Get<TreasureHunter>();
            entity.Get<TimerDoneEvent<FindTreasureTimer>>();

            return entity;
        }
        
        public EcsEntity SpawnTreasureDigger()
        {
            IWorkerData data = _configuration.TreasureDiggerData;
            Transform spawnPoint = _sceneData.TreasureDiggerSpawnPoint;

            EcsEntity entity = SpawnWorker(data, spawnPoint);

            entity.Get<TreasureDigger>();

            return entity;
        }
        
        public EcsEntity SpawnTreasureCollector()
        {
            IWorkerData data = _configuration.TreasureCollectorData;
            Transform spawnPoint = _sceneData.TreasureCollectorSpawnPoint;

            EcsEntity entity = SpawnWorker(data, spawnPoint);

            entity.Get<TreasureCollector>();

            return entity;
        }

        private EcsEntity SpawnWorker(IWorkerData data, Transform spawnPoint)
        {
            EcsEntity entity = _ecsWorld.NewEntity();
            WorkerView view = Object.Instantiate(data.Prefab, spawnPoint);
            //view.AttachEntity(entity);

            ref NavMeshAgentProvider navMeshAgentProvider = ref entity.Get<NavMeshAgentProvider>();
            navMeshAgentProvider.Value = view.NavMeshAgent;
            navMeshAgentProvider.Value.Warp(spawnPoint.position);
            navMeshAgentProvider.Value.speed = data.MovementSpeed;
            navMeshAgentProvider.Value.angularSpeed = data.RotationSpeed;

            entity.Replace(new TransformProvider {Value = view.transform});
            entity.Get<SpawnedThisFrameEvent>();

            return entity;
        }
    }
}