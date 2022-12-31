using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Static
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        public static readonly int VerticalHash = Animator.StringToHash("Vertical");
        [field: SerializeField] public TreasureHunterData TreasureHunterData { get; private set; }
        [field: SerializeField] public TreasureDiggerData TreasureDiggerData { get; private set; }
        [field: SerializeField] public TreasureCollectorData TreasureCollectorData { get; set; }
        [field: SerializeField] public GameObject FlagPrefab { get; private set; }
        [field: SerializeField] public GameObject TreasurePrefab { get; private set; }
        [field: SerializeField] public Image TimerViewPrefab { get; private set; }
        [field: SerializeField] public CameraData CameraData { get; private set; }
    }
}