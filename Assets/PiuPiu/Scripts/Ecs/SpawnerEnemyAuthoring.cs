using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class SpawnerEnemyAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        
        class Baker : Baker<SpawnerEnemyAuthoring>
        {
            public override void Bake(SpawnerEnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Spawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}