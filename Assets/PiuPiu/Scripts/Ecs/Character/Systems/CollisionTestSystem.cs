using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Stateful;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct CollisionTestSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CollisionTestData>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityManager = state.EntityManager;

            foreach (var (collisionTestData , entity) in 
                     SystemAPI.Query<RefRW<CollisionTestData>>().WithEntityAccess())
            {
                if (entityManager.HasComponent<StatefulTriggerEvent>(entity))
                {
                    collisionTestData.ValueRW.isCollided = true;
                    var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(entity);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        
                    }
                }
                else
                {
                    collisionTestData.ValueRW.isCollided = false;
                }
            }
        }
    }
}