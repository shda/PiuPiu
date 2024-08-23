using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character
{
    public class SpawnerCharacterAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        
        class Baker : Baker<SpawnerCharacterAuthoring>
        {
            public override void Bake(SpawnerCharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SpawnerData
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}