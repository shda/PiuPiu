using PiuPiu.Scripts.Ecs.Common;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Stateful;

namespace PiuPiu.Scripts.Ecs.Coin
{
    [BurstCompile]
    public partial struct CoinUpSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CoinUpData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            var entityManager = state.EntityManager;

            foreach (var (coinData, entity) in
                     SystemAPI.Query<RefRW<CoinUpData>>().WithEntityAccess())
            {
                if (SystemAPI.HasComponent<DestroyTag>(entity)) 
                    continue;
                
                if (entityManager.HasComponent<StatefulTriggerEvent>(entity))
                {
                    var buffer = state.EntityManager.GetBuffer<StatefulTriggerEvent>(entity);
                    foreach (var item in buffer)
                    {
                        var hitEntity = item.EntityA;
                        if (hitEntity == entity)
                        {
                            hitEntity = item.EntityB;
                        }
                        
                        if (SystemAPI.HasComponent<CoinCountData>(hitEntity))
                        {
                            var coinCount = SystemAPI.GetComponentRW<CoinCountData>(hitEntity);
                            coinCount.ValueRW.coinCount += coinData.ValueRO.countUp;
                            
                            ecb.AddComponent(entity, new DestroyTag());
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