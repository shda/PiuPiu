using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace PiuPiu.Scripts.Ecs.Character.Systems
{
    public partial struct BulletSpawnSystem : ISystem
    {
        private float currentTime;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputData>();
            state.RequireForUpdate<BulletSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            /*
            foreach (var (bulletSpawner , localTransform) in 
                     SystemAPI.Query<RefRW<BulletSpawner>, RefRO<LocalToWorld>>())
            {
                bulletSpawner.ValueRW.currentTime -= SystemAPI.Time.DeltaTime;

                if (bulletSpawner.ValueRW.currentTime <= 0)
                {
                    bulletSpawner.ValueRW.currentTime = bulletSpawner.ValueRO.delayToFire;
                    
                    var newEntity = state.EntityManager.Instantiate(bulletSpawner.ValueRO.BulletPrefab);
                    
                }
            }
    */
            
            /*
            float deltaTime = SystemAPI.Time.DeltaTime;
            currentTime -= deltaTime;
            
            if(currentTime > 0)
                return;
            
            var input = SystemAPI.GetSingleton<InputData>();
            if(!input.Space)
                return;
            
            
            currentTime = bulletSpawner.delayToFire;
            */
            
            /*
            var bulletSpawner = SystemAPI.GetSingleton<BulletSpawner>();
            
            
            var newEntity = state.EntityManager.Instantiate(bulletSpawner.BulletPrefab);
                
            var transform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
            transform.ValueRW.Position = localToWorld.ValueRO.Position;
            */
            //var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            // var randomFloat3 = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;
            // transform.ValueRW.Position = new float3(randomFloat3.x , 0 , randomFloat3.z);
            
        }
    }
}