using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class CollisionTestAuthoring : MonoBehaviour
    {
        class Baker : Baker<CollisionTestAuthoring>
        {
            public override void Bake(CollisionTestAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CollisionTestData());
            }
        }
    }
}