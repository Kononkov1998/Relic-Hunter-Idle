using MonoBehaviours;
using MonoBehaviours.Base;
using MonoBehaviours.View;

namespace Data.Static
{
    public interface IWorkerData
    {
        WorkerView Prefab { get; }
        float MovementSpeed { get; }
        float RotationSpeed { get; }
        int MaxCount { get; }
    }
}