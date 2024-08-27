using PiuPiu.Scripts.Ecs.Character;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Common
{
    public class SpawnRandomPrefabsAuthoring : MonoBehaviour
    {
        [SerializeField] private int Count;
        [SerializeField] private float3 offset = new(0.5f,0,0.5f);
        [SerializeField] private float factor = 5;
        [SerializeField] private GameObject[] Prefabs;
        
        class Baker : Baker<SpawnRandomPrefabsAuthoring>
        {
            public override void Bake(SpawnRandomPrefabsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                var buffer = AddBuffer<SpawnPrefab>(entity);
                foreach (var prefab in authoring.Prefabs)
                {
                    if (prefab != null)
                    {
                        buffer.Add(new SpawnPrefab()
                        {
                            Prefab = GetEntity(prefab, TransformUsageFlags.Dynamic)
                        });
                    }
                }

                var spawnData = new SpawnRandomPrefabsData
                {
                    factor = authoring.factor,
                    offset = authoring.offset,
                    Count = authoring.Count,
                };
                AddComponent(entity, spawnData);
            }
        }
    }
}