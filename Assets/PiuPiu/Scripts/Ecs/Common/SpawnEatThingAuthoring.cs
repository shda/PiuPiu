using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character
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