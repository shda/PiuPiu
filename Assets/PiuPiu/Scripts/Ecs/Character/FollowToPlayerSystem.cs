using PiuPiu.Scripts.Ecs.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character
{
    public partial struct FollowToPlayerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerData>();
            state.RequireForUpdate<CharacterMovingData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerData>();
            var localWorld = SystemAPI.GetComponentRO<LocalToWorld>(player);
            
            foreach (var (movingData, enemyLocalToWorld) in
                     SystemAPI.Query<RefRW<CharacterMovingData>, RefRW<LocalToWorld>>().WithNone<PlayerData>())
            {
                if (math.distance(movingData.ValueRW.MovingDirection, localWorld.ValueRO.Position) > 1)
                {
                    movingData.ValueRW.RotatePoint = localWorld.ValueRO.Position;

                    var direction =
                        math.normalizesafe(localWorld.ValueRO.Position - enemyLocalToWorld.ValueRO.Position);
                    
                    movingData.ValueRW.MovingDirection =  direction * movingData.ValueRW.Speed;
                }
                else
                {
                    movingData.ValueRW.MovingDirection = new float3();
                }
            }
        }
    }
}