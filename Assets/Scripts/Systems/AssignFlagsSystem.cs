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
    public class AssignFlagsSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private EcsFilter<TreasureDigger>.Exclude<FlagOwner> _diggers;
        private EcsFilter<Flag>.Exclude<Assigned> _flags;

        public void Run()
        {
            if (_diggers.IsEmpty() || _flags.IsEmpty()) return;

            EcsEntity diggerEntity = _diggers.GetEntity(0);
            EcsEntity flagEntity = _flags.GetEntity(0);
            NavMeshAgent agent = diggerEntity.Get<NavMeshAgentProvider>().Value;
            ref FlagOwner treasureOwner = ref diggerEntity.Get<FlagOwner>();

            flagEntity.Get<Assigned>();
            treasureOwner.Flag = flagEntity;
            
            Vector3 destination = flagEntity.Get<TransformProvider>().Value.position;
            destination.y = diggerEntity.Get<TransformProvider>().Value.position.y;
            agent.SetDestination(destination);
        }
    }
}