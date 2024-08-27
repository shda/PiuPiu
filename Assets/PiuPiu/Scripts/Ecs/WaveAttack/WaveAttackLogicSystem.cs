using PiuPiu.Scripts.Ecs.Common;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.WaveAttack
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct WaveAttackLogicSystem : ISystem
    {
        private Random random;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            random = Random.CreateFromIndex(111);
            state.RequireForUpdate<WaveAttackData>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (waveAttackData ,  entity) 
                     in SystemAPI.Query<RefRW<WaveAttackData>>().WithEntityAccess())
            {
                waveAttackData.ValueRW.currentDelayAfterAttack -= SystemAPI.Time.DeltaTime;
                
                if(waveAttackData.ValueRO.currentDelayAfterAttack > 0)
                    continue;
                waveAttackData.ValueRW.delayAfterAttack -= waveAttackData.ValueRO.downTimeAfterAttack;
                waveAttackData.ValueRW.currentDelayAfterAttack = waveAttackData.ValueRO.delayAfterAttack;
                
                if(waveAttackData.ValueRW.currentDelayAfterAttack < 0.1f)
                    waveAttackData.ValueRW.currentDelayAfterAttack = 0.1f;
                
                var spawnPointsBuffer = SystemAPI.GetBuffer<SpawnPoints>(entity);
                var spawnPrefabBuffer = SystemAPI.GetBuffer<SpawnPrefab>(entity);
                
                var nextPrefab = spawnPrefabBuffer[random.NextInt(0, spawnPrefabBuffer.Length)];
                var nextPoint = spawnPointsBuffer[random.NextInt(0, spawnPointsBuffer.Length)];
 
                var point = SystemAPI.GetComponent<LocalTransform>(nextPoint.Point);
                
                var newEntity = state.EntityManager.Instantiate(nextPrefab.Prefab);
                var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
                transform.ValueRW.Position = new float3(point.Position.x , 0 , point.Position.z);
            }
        }
    }
}