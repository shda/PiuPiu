using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Bullet
{
    public struct BulletSpawnerData : IComponentData
    {
        public float delayToFire;
        public float bulletSpeed;
        public Entity BulletPrefab;
        
        public float currentTime;
        
        public bool isFireing;
    }
}