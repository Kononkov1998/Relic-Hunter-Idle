using System;
using UnityEngine;

namespace Data.Static
{
    [Serializable]
    public class CameraData
    {
        [field: SerializeField] public float CameraSmoothTime { get; private set; }
        [field: SerializeField] public Vector3 CameraFollowOffset { get; private set; }
    }
}