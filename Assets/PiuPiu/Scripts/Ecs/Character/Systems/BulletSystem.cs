using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Stateful;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct BulletSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityManager = state.EntityManager;
            
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            foreach (var (bulletData, bulletEntity) in
                     SystemAPI.Query<RefRW<BulletData>>().WithEntityAccess())
            {
                bulletData.ValueRW.liveTime -= SystemAPI.Time.DeltaTime;
                if (bulletData.ValueRW.liveTime <= 0)
                {
                    ecb.AddComponent(bulletEntity , new DestroyComponentData());
                    continue;
                }
                
                if (entityManager.HasComponent<StatefulTriggerEvent>(bulletEntity))
                {
                    var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(bulletEntity);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        var item = buffer[i];

                        var hitEntity = item.EntityA;
                        if (hitEntity == bulletEntity)
                        {
                            hitEntity = item.EntityB;
                        }

                        if (SystemAPI.HasComponent<HealthComponentData>(hitEntity))
                        {
                            var health = SystemAPI.GetComponentRW<HealthComponentData>(hitEntity);
                            health.ValueRW.currentHealth -= bulletData.ValueRO.hitDamage;
                            
                            if (health.ValueRW.currentHealth <= 0)
                            {
                                ecb.AddComponent(hitEntity, new DestroyComponentData());
                            }
                            
                            //Destroy bullet
                            ecb.AddComponent(bulletEntity , new DestroyComponentData());
                        }
                    }
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
            
        }
    }
}