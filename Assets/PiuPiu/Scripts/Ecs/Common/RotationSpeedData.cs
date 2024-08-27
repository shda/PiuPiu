using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Common
{
    public struct RotationSpeedData : IComponentData
    {
        public float RadiansPerSecond;
    }
}