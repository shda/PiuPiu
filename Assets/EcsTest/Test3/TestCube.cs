using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EcsTest.Test3
{
    public class TestCube : MonoBehaviour
    {
        public float SpeedMove = 100;
        class Baker : Baker<TestCube>
        {
            public override void Bake(TestCube authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity , new TestCubeData
                {
                    SpeedMove = authoring.SpeedMove,
                });
            }
        }
    }


    public partial struct TestCubeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TestCubeData>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var movement in
                     SystemAPI.Query<VerticalMovementAspect>())
            {
                movement.Move(deltaTime);
            }

            /*
            // Loop over every entity having a LocalTransform component and RotationSpeed component.
            // In each iteration, transform is assigned a read-write reference to the LocalTransform,
            // and speed is assigned a read-only reference to the RotationSpeed component.
            foreach (var (transform, speed) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                // ValueRW and ValueRO both return a ref to the actual component value.
                // The difference is that ValueRW does a safety check for read-write access while
                // ValueRO does a safety check for read-only access.
                transform.ValueRW = transform.ValueRO.RotateY(
                    speed.ValueRO.RadiansPerSecond * deltaTime);
            }
            */
        }
    }
    
    readonly partial struct VerticalMovementAspect : IAspect
    {
        readonly RefRW<LocalTransform> m_Transform;
        readonly RefRO<TestCubeData> TestInfo;

        public void Move(double deltaTime)
        {
            m_Transform.ValueRW.Position.y += (float)deltaTime * TestInfo.ValueRO.SpeedMove;
        }
    }

    public struct TestCubeData : IComponentData
    {
        public float SpeedMove;
    }
}
