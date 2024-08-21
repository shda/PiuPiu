using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class BulletSpawnerAuthoring : MonoBehaviour
    {
        public float delayToFire;
        public float bulletSpeed;
        public GameObject BulletPrefab;
        
        class Baker : Baker<BulletSpawnerAuthoring>
        {
            public override void Bake(BulletSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletSpawnerData
                {
                    bulletSpeed = authoring.bulletSpeed,
                    delayToFire = authoring.delayToFire,
                    BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}