using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct DestroyEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DestroyComponentData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            foreach (var (destroyComponentData, entity) in
                     SystemAPI.Query<RefRO<DestroyComponentData>>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}