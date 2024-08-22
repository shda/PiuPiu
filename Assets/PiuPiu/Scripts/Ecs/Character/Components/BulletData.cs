using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Components
{
    public struct BulletData : IComponentData
    {
        public int hitDamage;
        public float liveTime;
    }
}