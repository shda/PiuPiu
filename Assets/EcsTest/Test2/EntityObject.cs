using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace EcsTest.Test2
{
    public class EntityObject : MonoBehaviour
    {
        public float RotateSpeed = 100;
        class Baker : Baker<EntityObject>
        {
            public override void Bake(EntityObject authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity , new RotationSpeed
                {
                    RadiansPerSecond = authoring.RotateSpeed,
                });
            }
        }
    }

    public partial struct RotateTestSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotationSpeed>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

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
        }
    }
    
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}
