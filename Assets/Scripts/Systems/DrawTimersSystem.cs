using System.Timers;
using Components;
using Components.Events;
using Components.Timers;
using Components.UnityProviders;
using Data.Runtime;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;

public class DrawTimersSystem<T> : IEcsRunSystem where T : struct
{
    private Configuration _configuration;
    private SceneData _sceneData;
    private RuntimeData _runtimeData;
    private EcsWorld _ecsWorld;
    private UI _ui;

    private EcsFilter<Timer<T>, TimerVisualRequest> _timersWithoutView;
    private EcsFilter<TimerVisual<T>> _visuals;

    public void Run()
    {
        foreach (int timerIndex in _timersWithoutView)
            SpawnTimerView(_timersWithoutView.GetEntity(timerIndex));

        foreach (int visualIndex in _visuals)
            UpdateTimerVisual(_visuals.GetEntity(visualIndex));
    }

    private void UpdateTimerVisual(EcsEntity entity)
    {
        ref TimerVisual<T> timerVisual = ref entity.Get<TimerVisual<T>>();
        Transform visualTransform = entity.Get<TransformProvider>().Value;
        Vector3 targetPosition = timerVisual.TimerEntity.Get<TransformProvider>().Value.position + Vector3.up * 5f;
        targetPosition = _sceneData.MainCamera.WorldToScreenPoint(targetPosition);
        visualTransform.position = targetPosition;

        if (timerVisual.TimerEntity.Has<TimerDoneEvent<T>>())
        {
            timerVisual.Image.fillAmount = 1;
            entity.Replace(new NeedDestroyNextFrame {GameObject = visualTransform.gameObject});
        }
        else
        {
            var timer = timerVisual.TimerEntity.Get<Timer<T>>();
            timerVisual.Image.fillAmount = 1 - timer.Value / timer.StartValue;
        }
    }

    private void SpawnTimerView(EcsEntity entity)
    {
        entity.Del<TimerVisualRequest>();
        var timer = entity.Get<Timer<T>>();
        EcsEntity visualEntity = _ecsWorld.NewEntity();
        
        Image view = Object.Instantiate(_configuration.TimerViewPrefab, _ui.ActionTimersCanvas);
        view.GetComponent<EntityView>().AttachEntity(visualEntity);
        view.fillAmount = 1 - timer.Value / timer.StartValue;
        
        visualEntity.Replace(new TimerVisual<T>
        {
            TimerEntity = entity,
            Image = view
        });
        visualEntity.Replace(new TransformProvider {Value = view.transform});
    }
}