using System;
using MonoBehaviours.View;
using UnityEngine;

namespace Data.Static
{
    [Serializable]
    public class TreasureDiggerData : IWorkerData
    {
        [field: SerializeField] public WorkerView Prefab { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float SqrDigTreasureRange { get; private set; }
        [field: SerializeField] public float DigTime { get; private set; }
    }
}