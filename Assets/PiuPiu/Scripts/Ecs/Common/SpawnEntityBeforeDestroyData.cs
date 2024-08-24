using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct SpawnEntityBeforeDestroyData : IComponentData
    {
        public Entity Prefab;
        public bool isAlreadySpawn;
    }
    
    [UpdateBefore(typeof(DestroyEntitySystem))]
    public partial struct SpawnEntityBeforeDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnEntityBeforeDestroyData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (prefabData , localTransform , entity) 
                     in SystemAPI.Query<RefRW<SpawnEntityBeforeDestroyData> , RefRO<LocalTransform>>().
                         WithAll<DestroyTag>().WithEntityAccess())
            {
                if(prefabData.ValueRW.isAlreadySpawn)
                    continue;

                prefabData.ValueRW.isAlreadySpawn = true;
                
                var newEntity = state.EntityManager.Instantiate(prefabData.ValueRO.Prefab);
                var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
                transform.ValueRW.Position = localTransform.ValueRO.Position;
            }
        }
    }
}