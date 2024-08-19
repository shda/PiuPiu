using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace EcsTest
{
    public class RotateObject : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;
        class Baker : Baker<RotateObject>
        {
            public override void Bake(RotateObject authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
    }
    
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}