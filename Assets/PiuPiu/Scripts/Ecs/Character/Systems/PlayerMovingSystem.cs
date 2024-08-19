using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct PlayerMovingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var input = SystemAPI.GetSingleton<InputData>();
            
            foreach (var movingData in
                     SystemAPI.Query<RefRW<CharacterMovingData>>().WithAny<PlayerData>())
            {
                // var move = new float3(input.Horizontal, 0, input.Vertical);
                movingData.ValueRW.Direction =
                    new float3(input.Horizontal, movingData.ValueRW.Direction.y, input.Vertical);
            }
        }
    }
}