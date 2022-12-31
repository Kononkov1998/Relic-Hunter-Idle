using Components;
using Components.UnityProviders;
using Data;
using Data.Scene;
using Data.Static;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private Configuration _configuration;

        private Vector3 _currentVelocity;
        private EcsFilter<TreasureHunter, TransformProvider> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (int index in _filter)
            {
                ref TransformProvider transformProvider = ref _filter.Get2(index);

                Vector3 currentPosition = _sceneData.MainCamera.transform.position;
                currentPosition = Vector3.SmoothDamp(currentPosition,
                    transformProvider.Value.position + _configuration.CameraData.CameraFollowOffset,
                    ref _currentVelocity, _configuration.CameraData.CameraSmoothTime);
                _sceneData.MainCamera.transform.position = currentPosition;
            }
        }
    }
}