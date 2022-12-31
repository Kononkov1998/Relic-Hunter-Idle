using System;
using Leopotam.Ecs.UnityIntegration;
using MonoBehaviours;

[Serializable]
public class MyEntityObserver
{
    public EntityView EntityView;
    public EcsEntityObserver EntityObserver;
}