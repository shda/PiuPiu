using PiuPiu.Scripts.Ecs.Character;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Common
{
    public partial struct SpawnRandomPrefabsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnRandomPrefabsData>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var random = Random.CreateFromIndex(111);

            foreach (var (spawnPrefabData , entity) 
                     in SystemAPI.Query<RefRW<SpawnRandomPrefabsData>>().WithEntityAccess())
            {
                if(spawnPrefabData.ValueRW.isAlreadySpawn)
                    continue;

                spawnPrefabData.ValueRW.isAlreadySpawn = true;
                
                var buffer = SystemAPI.GetBuffer<SpawnPrefab>(entity);
                if(buffer.IsEmpty)
                    continue;
                
                for (int i = 0; i < spawnPrefabData.ValueRW.Count; i++)
                {
                    var prefabs = random.NextInt(0, buffer.Length);
                    var nextPrefab = buffer[prefabs];

                    var newEntity = state.EntityManager.Instantiate(nextPrefab.Prefab);
                    var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
                    var randomFloat3 = (random.NextFloat3() - spawnPrefabData.ValueRW.offset) * spawnPrefabData.ValueRW.factor;
                    transform.ValueRW.Position = new float3(randomFloat3.x , 0 , randomFloat3.z);
                }
            }
        }
    }
}