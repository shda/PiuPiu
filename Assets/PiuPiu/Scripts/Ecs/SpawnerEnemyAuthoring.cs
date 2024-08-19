using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class SpawnerEnemyAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        // In baking, this Baker will run once for every SpawnerAuthoring instance in a subscene.
        // (Note that nesting an authoring component's Baker class inside the authoring MonoBehaviour class
        // is simply an optional matter of style.)
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