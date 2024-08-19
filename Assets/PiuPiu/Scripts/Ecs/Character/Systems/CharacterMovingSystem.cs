using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct CharacterMovingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CharacterMovingData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (physicsVelocity, movingData) in
                     SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<CharacterMovingData>>())
            {
                physicsVelocity.ValueRW.Linear = movingData.ValueRO.Direction * movingData.ValueRO.Speed * deltaTime;
                physicsVelocity.ValueRW.Angular = new float3();
            }
        }
    }
}
