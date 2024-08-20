using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Components
{
    public struct BulletSpawner : IComponentData
    {
        public float delayToFire;
        public float bulletSpeed;
        public Entity BulletPrefab;
        
        public float currentTime;
    }
}