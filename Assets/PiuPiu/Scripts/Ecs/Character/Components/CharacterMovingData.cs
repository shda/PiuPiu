using Unity.Entities;
using Unity.Mathematics;

namespace PiuPiu.Scripts.Ecs.Character.Components
{
    public struct CharacterMovingData : IComponentData
    {
        public float Speed;
        public float3 MovingDirection;
        public float3 RotatePoint;
    }
}