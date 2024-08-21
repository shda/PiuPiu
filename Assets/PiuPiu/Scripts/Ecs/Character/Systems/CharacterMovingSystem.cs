using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    [UpdateAfter(typeof(PhysicsInitializeGroup)), UpdateBefore(typeof(ExportPhysicsWorld))]
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
            
            foreach (var (physicsVelocity, movingData , entity) in
                     SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<CharacterMovingData>>().WithEntityAccess())
            {
               physicsVelocity.ValueRW.Linear = movingData.ValueRO.MovingDirection * movingData.ValueRO.Speed * deltaTime;
               physicsVelocity.ValueRW.Angular = new float3();
               
               var localTransform = SystemAPI.GetComponentRW<LocalTransform>(entity);

               var point = movingData.ValueRO.RotatePoint - localTransform.ValueRW.Position;
               var rotate = quaternion.LookRotationSafe(point, math.up());
               
               var euler = math.Euler(rotate);
               var start = math.Euler(localTransform.ValueRW.Rotation);

              localTransform.ValueRW.Rotation = quaternion.Euler(start.x ,euler.y , euler.z);
            }
        }
    }
}
