using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character
{
    [UpdateAfter(typeof(PhysicsInitializeGroup)), UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial struct MovingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MovingData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
           // float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (physicsVelocity,
                         movingData,
                         localTransform,
                         entity) in
                     SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MovingData>, RefRW<LocalTransform>>()
                         .WithEntityAccess())
            {
                physicsVelocity.ValueRW.Linear =
                    movingData.ValueRO.MovingDirection * movingData.ValueRO.Speed;// * deltaTime;
                physicsVelocity.ValueRW.Angular = new float3();

                var point = movingData.ValueRO.RotateToDirection - localTransform.ValueRW.Position;
                var rotate = quaternion.LookRotationSafe(point, math.up());

                var euler = math.Euler(rotate);
                var start = math.Euler(localTransform.ValueRW.Rotation);

                localTransform.ValueRW.Rotation = quaternion.Euler(start.x, euler.y, euler.z);
                localTransform.ValueRW.Position = new float3(
                    localTransform.ValueRW.Position.x, 0,
                    localTransform.ValueRW.Position.z);
            }
        }
    }
}