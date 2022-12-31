using System;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace MonoBehaviours
{
    public class EntityView : MonoBehaviour
    {
        private EcsEntity _entity;

        public void AttachEntity(EcsEntity entity)
        {
            _entity = entity;
        }

        private void AssignSelfToEntity()
        {
            FindObjectOfType<MyEntitiesObserver>().AssignSelf(this, _entity);
        }

#if UNITY_EDITOR
        [Button]
        private void SelectEntity()
        {
            GameObject selectedEntity = FindObjectOfType<EcsWorldObserver>().EntityGameObjects[_entity.GetInternalId()];
            if (selectedEntity != null)
                Selection.activeGameObject = selectedEntity;
            else
                Debug.LogError($"Entity of {gameObject.name} is null!");
        }
#endif
    }
}