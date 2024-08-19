using Unity.Entities;
using UnityEngine;

namespace EcsTest
{
    public struct LevelComponent : IComponentData
    {
        public float level;
    }

    public class LevelSystem : ComponentSystemBase
    {
        public override void Update()
        {
            
        }
    }

    public class Testing : MonoBehaviour
    {
        void Start()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            for (int i = 0; i < 100000; i++)
            {
                var entity = entityManager.CreateEntity(typeof(LevelComponent));
                entityManager.SetName(entity, $"Entityyyy {i}");
                entityManager.SetComponentData(entity , new LevelComponent { level = i });
            }
        
        

            Debug.Log("Hello World!");
        }
    }
}