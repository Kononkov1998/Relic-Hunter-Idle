using NaughtyAttributes;
using UnityEngine;

namespace MonoBehaviours
{
    public class DevHelper : MonoBehaviour
    {
        [Button]
        public void IncreaseMaxHunters()
        {
            GetComponent<RuntimeDataView>().Data.MaxTreasureHuntersCount++;
        }

        [Button]
        public void IncreaseMaxCollectors()
        {
            GetComponent<RuntimeDataView>().Data.MaxTreasureCollectorsCount++;
        }
    }
}