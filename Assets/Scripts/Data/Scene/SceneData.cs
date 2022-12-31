using MonoBehaviours;
using UnityEngine;

namespace Data.Scene
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public Transform TreasureHunterSpawnPoint { get; private set; }
        [field: SerializeField] public Transform TreasureDiggerSpawnPoint { get; private set; }
        [field: SerializeField] public Transform TreasureCollectorSpawnPoint { get; private set; }
        [field: SerializeField] public RuntimeDataView RuntimeDataView { get; private set; }
        [field: SerializeField] public Transform FlagParent { get; private set; }
        [field: SerializeField] public Transform TreasureParent { get; private set; }
    }
}