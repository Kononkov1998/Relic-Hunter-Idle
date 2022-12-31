using System;
using MonoBehaviours;
using MonoBehaviours.Base;
using MonoBehaviours.View;
using UnityEngine;
using EntityView = MonoBehaviours.EntityView;

namespace Data.Static
{
    [Serializable]
    public class TreasureHunterData : IWorkerData
    {
        [field: SerializeField] public WorkerView Prefab { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float WaitTimeBetweenWalking { get; private set; }
        [field: SerializeField] public float WalkRadius { get; private set; }

        [field: SerializeField]
        [field: Range(0, 100f)]
        public float ChanceToFindTreasure { get; private set; }
    }
}