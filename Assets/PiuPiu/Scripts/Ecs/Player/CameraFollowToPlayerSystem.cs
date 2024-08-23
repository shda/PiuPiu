using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Player
{
    public partial struct CameraFollowToPlayerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerCameraData>();
        }

      //  [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var transformCameraData in SystemAPI.Query<TransformCameraData>())
            {
                foreach (var (playerData, localToWorld , entity) 
                         in SystemAPI.Query<RefRO<PlayerData>, RefRW<LocalToWorld>>().WithEntityAccess())
                {
                    transformCameraData.Transform.position = localToWorld.ValueRO.Position;
                }
            }
        }
    }
}