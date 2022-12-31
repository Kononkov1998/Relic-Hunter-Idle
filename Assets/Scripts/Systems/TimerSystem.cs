using Components.Events;
using Components.Timers;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class TimerSystem<T> : IEcsRunSystem where T : struct
    {
        private EcsFilter<Timer<T>> _timers;
        private EcsFilter<TimerDoneEvent<T>> _doneEvents;

        public void Run()
        {
            foreach (int index in _doneEvents)
                _doneEvents.GetEntity(index).Del<TimerDoneEvent<T>>();

            foreach (int index in _timers)
            {
                EcsEntity entity = _timers.GetEntity(index);
                ref Timer<T> timer = ref entity.Get<Timer<T>>();
                timer.Value -= Time.deltaTime;
                if (timer.Value <= 0f)
                {
                    entity.Del<Timer<T>>();
                    entity.Get<TimerDoneEvent<T>>();
                }
            }
        }
    }
}