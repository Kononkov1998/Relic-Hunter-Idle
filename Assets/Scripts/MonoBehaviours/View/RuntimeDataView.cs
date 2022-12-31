using Data.Runtime;
using NaughtyAttributes;
using UnityEngine;

namespace MonoBehaviours
{
    public class RuntimeDataView : MonoBehaviour
    {
        [field: SerializeField]
        [field: ReadOnly]
        public RuntimeData Data { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}