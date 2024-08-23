using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Bullet
{
    public struct BulletData : IComponentData
    {
        public int hitDamage;
        public float liveTime;
    }
}