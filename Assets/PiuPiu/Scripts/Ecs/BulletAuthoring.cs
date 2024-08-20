using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class BulletAuthoring : MonoBehaviour
    {
        class Baker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletData());
            }
        }
    }
}