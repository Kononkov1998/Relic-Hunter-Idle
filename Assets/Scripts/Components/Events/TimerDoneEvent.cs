using Leopotam.Ecs;

namespace Components.Events
{
    public struct TimerDoneEvent<TTag> : IEcsIgnoreInFilter where TTag : struct
    {
    }
}