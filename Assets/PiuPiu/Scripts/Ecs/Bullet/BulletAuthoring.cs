using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Bullet
{
    public class BulletAuthoring : MonoBehaviour
    {
        [SerializeField] private int hitDamage = 100;
        [SerializeField] private float liveTime = 5.0f;
        class Baker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletData
                {
                    hitDamage = authoring.hitDamage,
                    liveTime = authoring.liveTime,
                });
            }
        }
    }
}