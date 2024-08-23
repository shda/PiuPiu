using PiuPiu.Scripts.Ecs.Character;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace PiuPiu.Scripts.Ecs.Player
{
    public partial struct PlayerMovingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerRotateMouseData>();
            state.RequireForUpdate<InputData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var input = SystemAPI.GetSingleton<InputData>();
            
            foreach (var movingData in
                     SystemAPI.Query<RefRW<CharacterMovingData>>().WithAny<PlayerData>())
            {
                movingData.ValueRW.MovingDirection =
                    new float3(input.Horizontal, movingData.ValueRW.MovingDirection.y, input.Vertical);
            }
        }
    }
}