using Components.Timers;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public struct TimerVisual<T> : IEcsIgnoreInFilter where T : struct
    {
        public EcsEntity TimerEntity;
        public Image Image;
    }
}