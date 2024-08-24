using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Stateful;

namespace PiuPiu.Scripts.Ecs.Bullet
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
                if(SystemAPI.HasComponent<DestroyTag>(bulletEntity))
                    continue;
                
                bulletData.ValueRW.liveTime -= SystemAPI.Time.DeltaTime;
                if (bulletData.ValueRW.liveTime <= 0)
                {
                    ecb.AddComponent(bulletEntity , new DestroyTag());
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

                        //Todo
                        if (SystemAPI.HasComponent<PlayerData>(hitEntity))
                        {
                            continue;
                        }

                        if (SystemAPI.HasComponent<HealthData>(hitEntity))
                        {
                            var health = SystemAPI.GetComponentRW<HealthData>(hitEntity);
                            health.ValueRW.health -= bulletData.ValueRO.hitDamage;
                            
                            if (health.ValueRW.health <= 0)
                            {
                                ecb.AddComponent(hitEntity, new DestroyTag());
                            }
                            
                            //Destroy bullet
                            ecb.AddComponent(bulletEntity , new DestroyTag());
                            break;
                        }
                    }
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
            
        }
    }
}