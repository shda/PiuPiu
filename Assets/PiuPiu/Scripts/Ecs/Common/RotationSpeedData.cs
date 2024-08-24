using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct RotationSpeedData : IComponentData
    {
        public float RadiansPerSecond;
    }
}