using Components;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Systems
{
    public class AssignTreasureSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private EcsFilter<TreasureCollector>.Exclude<TreasureOwner> _collectors;
        private EcsFilter<Treasure>.Exclude<Assigned> _treasures;

        public void Run()
        {
            if (_collectors.IsEmpty() || _treasures.IsEmpty()) return;
            
            EcsEntity collectorEntity = _collectors.GetEntity(0);
            EcsEntity treasureEntity = _treasures.GetEntity(0);
            NavMeshAgent collectorAgent = collectorEntity.Get<NavMeshAgentProvider>().Value;
            ref TreasureOwner treasureOwner = ref collectorEntity.Get<TreasureOwner>();
            
            treasureEntity.Get<Assigned>();
            treasureOwner.Treasure = treasureEntity;
            
            Vector3 destination = treasureEntity.Get<TransformProvider>().Value.position;
            destination.y = collectorEntity.Get<TransformProvider>().Value.position.y;
            collectorAgent.SetDestination(destination);
        }
    }
}