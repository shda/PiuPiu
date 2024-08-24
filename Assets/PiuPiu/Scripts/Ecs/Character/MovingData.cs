using Unity.Entities;
using Unity.Mathematics;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct MovingData : IComponentData
    {
        public float Speed;
        public float3 MovingDirection;
        public float3 RotateToDirection;
    }
}