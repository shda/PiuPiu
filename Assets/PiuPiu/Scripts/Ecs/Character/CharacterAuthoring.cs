using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject SpawnAfterDestroyPrefab;
        
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovingData
                {
                    MovingDirection = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });
                
                AddComponent(entity , new HealthData
                {
                    maxHealth = authoring.maxHealth,
                    health = authoring.currentHealth,
                });
                
                AddComponent(entity , new SpawnEntityBeforeDestroyData()
                {
                    Prefab = GetEntity(authoring.SpawnAfterDestroyPrefab , TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}
