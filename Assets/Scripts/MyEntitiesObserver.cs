using System.Collections.Generic;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using MonoBehaviours;
using UnityEngine;

public class MyEntitiesObserver : MonoBehaviour, IEcsWorldDebugListener
{
    public Dictionary<int, GameObject> EntityGameObjects;
    public List<MyEntityObserver> MyEntityObservers;

    public static void Create()
    {
        var go = new GameObject("[MyWorldObserver]");
        DontDestroyOnLoad(go);
        go.hideFlags = HideFlags.NotEditable;
        var observer = go.AddComponent<MyEntitiesObserver>();
        observer.EntityGameObjects = FindObjectOfType<EcsWorldObserver>().EntityGameObjects;
        observer.MyEntityObservers = new List<MyEntityObserver>();
    }

    public void AssignSelf(EntityView entityView, EcsEntity entity)
    {
        GameObject entityGameObject = GetEntityGameObject(entity);
        var myEntityObserver = new MyEntityObserver
        {
            EntityView = entityView,
            EntityObserver = entityGameObject.GetComponent<EcsEntityObserver>()
        };
        MyEntityObservers.Add(myEntityObserver);
    }

    public void OnEntityCreated(EcsEntity entity)
    {
    }

    public void OnEntityDestroyed(EcsEntity entity)
    {
    }

    public void OnFilterCreated(EcsFilter filter)
    {
    }

    public void OnComponentListChanged(EcsEntity entity)
    {
    }

    public void OnWorldDestroyed(EcsWorld world)
    {
    }

    private GameObject GetEntityGameObject(EcsEntity entity)
    {
        return EntityGameObjects[entity.GetInternalId()];
    }
}