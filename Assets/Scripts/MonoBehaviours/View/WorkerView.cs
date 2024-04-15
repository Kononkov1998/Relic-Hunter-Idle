using MonoBehaviours.Base;
using UnityEngine;
using UnityEngine.AI;

namespace MonoBehaviours.View
{
    public class WorkerView : MonoEntity
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, NavMeshAgent.destination);
            Gizmos.DrawSphere(NavMeshAgent.destination, 0.1f);
        }
    }
}