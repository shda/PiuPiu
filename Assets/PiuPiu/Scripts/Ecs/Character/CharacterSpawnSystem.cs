using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character
{
    public partial struct CharacterSpawnSystem : ISystem
    {
        bool isAlreadySpawning;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            isAlreadySpawning = false;
            state.RequireForUpdate<SpawnerData>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if(isAlreadySpawning)
                return;

            isAlreadySpawning = true;
            
            var prefab = SystemAPI.GetSingleton<SpawnerData>().Prefab;
            var instances = state.EntityManager.Instantiate(prefab, 50, Allocator.Temp);
            
            var random = Random.CreateFromIndex(111);
            
            foreach (var entity in instances)
            {
                // Update the entity's LocalTransform component with the new position.
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                var randomFloat3 = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 50;
                transform.ValueRW.Position = new float3(randomFloat3.x , 0 , randomFloat3.z);
            }
        }
    }
}