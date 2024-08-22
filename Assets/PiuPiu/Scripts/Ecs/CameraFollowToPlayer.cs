using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class CameraFollowToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        
        EntityQuery _CameraProxyQuery;
        
        void Start()
        {
            _CameraProxyQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
                new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<PlayerCameraData>());
        }
        
        void OnDestroy()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_CameraProxyQuery))
                _CameraProxyQuery.Dispose();
        }
        
        private void Update()
        {
            if (World.DefaultGameObjectInjectionWorld?.IsCreated == true &&
                World.DefaultGameObjectInjectionWorld.EntityManager.IsQueryValid(_CameraProxyQuery) &&
                !_CameraProxyQuery.IsEmpty)
            {
                var cameraEntity = _CameraProxyQuery.GetSingletonEntity();
                
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                entityManager.AddComponentData(cameraEntity , new TransformCameraData()
                {
                   Transform = mainCamera.transform,
                });
            }
        }
    }
}