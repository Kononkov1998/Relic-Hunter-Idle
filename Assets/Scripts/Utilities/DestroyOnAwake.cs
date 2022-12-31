using UnityEngine;

namespace Utilities
{
    public class DestroyOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject);
        }
    }
}