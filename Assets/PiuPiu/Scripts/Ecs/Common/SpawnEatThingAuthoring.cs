using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.EatThings;
using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Common
{
    public class SpawnEatThingAuthoring  : MonoBehaviour
    {
        [SerializeField] private GameObject eatThingPrefab;
        class Baker : Baker<SpawnEatThingAuthoring>
        {
            public override void Bake(SpawnEatThingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity , new EatThingSpawnData
                {
                    Prefab = GetEntity(authoring.eatThingPrefab, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}