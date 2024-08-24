using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character
{
    public class WaveAttackLogicAuthoring : MonoBehaviour
    {
        [SerializeField] private float delayAfterAttack = 10f;
        [SerializeField] private float downTimeAfterAttack = 0.2f;
        [SerializeField] private GameObject[] spawnPoint;
        [SerializeField] private GameObject[] enemyPrefabs;
        
        class Baker : Baker<WaveAttackLogicAuthoring>
        {
            public override void Bake(WaveAttackLogicAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                var bufferSp = AddBuffer<SpawnPoints>(entity);
                foreach (var point in authoring.spawnPoint)
                {
                    if (point != null)
                    {
                        bufferSp.Add(new SpawnPoints
                        {
                            Point = GetEntity(point, TransformUsageFlags.Dynamic)
                        });
                    }
                }
                
                var buffer = AddBuffer<SpawnPrefab>(entity);
                foreach (var prefab in authoring.enemyPrefabs)
                {
                    if (prefab != null)
                    {
                        buffer.Add(new SpawnPrefab
                        {
                            Prefab = GetEntity(prefab, TransformUsageFlags.Dynamic)
                        });
                    }
                }
                
                AddComponent(entity , new WaveAttackData
                {
                    delayAfterAttack = authoring.delayAfterAttack, 
                    downTimeAfterAttack = authoring.downTimeAfterAttack,
                    currentDelayAfterAttack = 0,
                });
            }
        }
    }
}