using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Player;
using Unity.Burst;
using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Common
{
    [UpdateBefore(typeof(PlayerMovingSystem))]
    public partial struct DestroyEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DestroyTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (destroyComponentData, entity) in
                     SystemAPI.Query<RefRO<DestroyTag>>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
        }
    }
}