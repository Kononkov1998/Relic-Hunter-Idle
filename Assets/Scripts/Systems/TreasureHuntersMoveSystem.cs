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

namespace Systems
{
    public class TreasureHuntersMoveSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;

        private EcsFilter<TreasureHunter>.Exclude<Timer<FindTreasureTimer>, TimerDoneEvent<FindTreasureTimer>> _treasureHunters;
        private EcsFilter<TreasureHunter, TimerDoneEvent<FindTreasureTimer>> _doneStayingHunters;

        public void Run()
        {
            foreach (int index in _treasureHunters)
            {
                EcsEntity entity = _treasureHunters.GetEntity(index);
                ref NavMeshAgent navMeshAgent = ref entity.Get<NavMeshAgentProvider>().Value;
                ref Transform transform = ref entity.Get<TransformProvider>().Value;

                if (Vector3.Distance(navMeshAgent.destination, transform.position) <= navMeshAgent.stoppingDistance)
                {
                    ref Timer<FindTreasureTimer> timer = ref entity.Get<Timer<FindTreasureTimer>>();
                    timer.StartValue = _configuration.TreasureHunterData.WaitTimeBetweenWalking;
                    timer.Value = timer.StartValue;
                    entity.Get<TimerVisualRequest>();
                }
            }

            foreach (int index in _doneStayingHunters)
            {
                EcsEntity entity = _doneStayingHunters.GetEntity(index);
                Vector3 position = entity.Get<TransformProvider>().Value.position;
                Vector3 newPosition = RandomNavmeshLocation(_configuration.TreasureHunterData.WalkRadius, position);
                entity.Get<NavMeshAgentProvider>().Value.SetDestination(newPosition);
            }
        }

        private static Vector3 RandomNavmeshLocation(float radius, Vector3 position)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius + position;
            Vector3 finalPosition = position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
                finalPosition.y = position.y;
            }

            return finalPosition;
        }
    }
}