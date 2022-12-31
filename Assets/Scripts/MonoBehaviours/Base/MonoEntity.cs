using Components.UnityProviders;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using NaughtyAttributes;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MonoBehaviours.Base
{
    public class MonoEntity : MonoBehaviour
    {
        private EcsEntity _entity;

        public void Make(ref EcsEntity entity)
        {
            _entity = entity;

            foreach (IMonoLink monoLink in GetComponents<IMonoLink>())
                monoLink.Make(ref entity);

            entity.Get<GameObjectProvider>() = new GameObjectProvider {Value = gameObject};
            entity.Get<TransformProvider>() = new TransformProvider {Value = transform};
        }

#if UNITY_EDITOR
        [Button]
        private void SelectEntity()
        {
            Selection.activeGameObject =
                FindObjectOfType<EcsWorldObserver>().EntityGameObjects[_entity.GetInternalId()];
        }
#endif
    }
}