using PiuPiu.Scripts.Ecs.Character;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Common
{
    public struct SpawnEntityBeforeDestroyData : IComponentData
    {
        public bool isAlreadySpawn;
    }
    
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateBefore(typeof(DestroyEntitySystem))]
    public partial struct SpawnEntityBeforeDestroySystem : ISystem
    {
        private Random random;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            random = Random.CreateFromIndex(111);
            state.RequireForUpdate<SpawnEntityBeforeDestroyData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (prefabData, localTransform, entity)
                     in SystemAPI.Query<RefRW<SpawnEntityBeforeDestroyData>, RefRO<LocalTransform>>()
                         .WithAll<DestroyTag>().WithEntityAccess())
            {
                if (prefabData.ValueRW.isAlreadySpawn)
                    continue;

                prefabData.ValueRW.isAlreadySpawn = true;

                var buffer = SystemAPI.GetBuffer<SpawnPrefab>(entity);
                if (buffer.IsEmpty)
                    continue;
                
                var prefabs = random.NextInt(0, buffer.Length);
                var nextPrefab = buffer[prefabs];

                var newEntity = state.EntityManager.Instantiate(nextPrefab.Prefab);
                var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
                transform.ValueRW.Position = localTransform.ValueRO.Position;
            }
        }
    }
}