using Components.UnityProviders;
using Leopotam.Ecs;
using MonoBehaviours.Base;
using UnityEngine;
using UnityEngine.AI;

namespace MonoBehaviours
{
    public class NavMeshAgentMono : MonoBehaviour, IMonoLink
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public void Make(ref EcsEntity entity)
        {
            entity.Replace(new NavMeshAgentProvider
            {
                Value = _navMeshAgent
            });
        }
    }
}