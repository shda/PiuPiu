using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Stateful;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Things
{
    public partial struct EatThingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EatThingData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            var entityManager = state.EntityManager;

            foreach (var (eatThingData, entity) in
                     SystemAPI.Query<RefRW<EatThingData>>().WithEntityAccess())
            {
                if (entityManager.HasComponent<StatefulTriggerEvent>(entity))
                {
                    var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(entity);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        var item = buffer[i];

                        var hitEntity = item.EntityA;
                        if (hitEntity == entity)
                        {
                            hitEntity = item.EntityB;
                        }
                        
                        if (SystemAPI.HasComponent<PlayerData>(hitEntity))
                        {
                            var health = SystemAPI.GetComponentRW<HealthData>(hitEntity);
                            health.ValueRW.health += eatThingData.ValueRO.upHealth;
                            if (health.ValueRW.health > health.ValueRO.maxHealth)
                                health.ValueRW.health = health.ValueRO.maxHealth;
                            
                            ecb.AddComponent(entity, new DestroyTag());
                        }
                    }
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}