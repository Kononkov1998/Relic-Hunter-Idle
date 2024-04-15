using Components.Events;
using Components.Timers;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Factories;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using MonoBehaviours;
using Services.SaveLoad;
using Systems;
using UnityEngine;

public sealed class EcsStartup : MonoBehaviour
{
    public Configuration Configuration;
    public SceneData SceneData;
    public UI UI;

    private RuntimeData _runtimeData;
    private EcsSystems _initSystems;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;
    private EcsSystems _lateUpdateSystems;
    private EcsWorld _world;

    private ISaveLoadService _saveLoadService;
    private WorkerFactory _workerFactory;

    private void Start()
    {
        _world = new EcsWorld();
        _initSystems = new EcsSystems(_world);
        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystems = new EcsSystems(_world);
        _lateUpdateSystems = new EcsSystems(_world);
        _runtimeData = new RuntimeData();

        _saveLoadService = new SaveLoadService();
        _workerFactory = new WorkerFactory(Configuration, SceneData, _world);

#if UNITY_EDITOR
        EcsWorldObserver.Create(_world);
        EcsSystemsObserver.Create(_updateSystems);
        MyEntitiesObserver.Create();
#endif
        _initSystems
            .Add(new RuntimeDataLoadSystem())
            .Add(new RuntimeDataViewSystem())
            .Inject(Configuration)
            .Inject(SceneData)
            .Inject(_runtimeData)
            .Inject(_saveLoadService)
            .Init();

        _updateSystems
            .Add(new DestroySystem())
            
            .Add(new SpawnTreasureHuntersSystem())
            .Add(new TreasureHuntersMoveSystem())
            .Add(new FindTreasureSystem())
            
            .Add(new SpawnTreasureDiggersSystem())
            .Add(new AssignFlagsSystem())
            .Add(new DigTreasureSystem())
            
            .Add(new SpawnTreasureCollectorsSystem())
            .Add(new AssignTreasureSystem())
            .Add(new CollectTreasureSystem())
            
            .Add(new UpdateUISystemTest())
            .Add(new DrawTimersSystem<FindTreasureTimer>())
            .Add(new DrawTimersSystem<DigTreasureTimer>())
            .Add(new DrawTimersSystem<CollectTreasureTimer>())
            .Add(new TimerSystem<FindTreasureTimer>())
            .Add(new TimerSystem<DigTreasureTimer>())
            .Add(new TimerSystem<CollectTreasureTimer>())
            .OneFrame<SpawnedThisFrameEvent>()
            .Inject(Configuration)
            .Inject(SceneData)
            .Inject(_runtimeData)
            .Inject(UI)
            .Inject(_workerFactory)
            .Init();

        _fixedUpdateSystems
            .Inject(Configuration)
            .Inject(SceneData)
            .Init();

        _lateUpdateSystems
            .Inject(Configuration)
            .Inject(SceneData)
            .Inject(_runtimeData)
            .Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void LateUpdate()
    {
        _lateUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _initSystems?.Destroy();
        _initSystems = null;
        _updateSystems?.Destroy();
        _updateSystems = null;
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        _lateUpdateSystems?.Destroy();
        _lateUpdateSystems = null;
        _world.Destroy();
        _world = null;
    }
}