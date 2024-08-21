using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
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
            state.RequireForUpdate<BulletSpawnerData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (bulletSpawner, localTransform, entity) in
                     SystemAPI.Query<RefRW<BulletSpawnerData>, RefRO<LocalToWorld>>().WithEntityAccess())
            {
                bulletSpawner.ValueRW.currentTime -= SystemAPI.Time.DeltaTime;
                
                if (bulletSpawner.ValueRW.currentTime <= 0)
                {

                    if (SystemAPI.HasComponent<Parent>(entity))
                    {
                        var parent = SystemAPI.GetComponent<Parent>(entity);
                        var inputData = SystemAPI.GetComponent<InputData>(parent.Value);
                        if(!inputData.Space)
                            return;
                    }

                    bulletSpawner.ValueRW.currentTime = bulletSpawner.ValueRO.delayToFire;

                    var newEntity = state.EntityManager.Instantiate(bulletSpawner.ValueRO.BulletPrefab);

                    var newLocalTransform = SystemAPI.GetComponentRW<LocalTransform>(newEntity);
                    newLocalTransform.ValueRW.Position = localTransform.ValueRO.Position;
                    newLocalTransform.ValueRW.Rotation = localTransform.ValueRO.Rotation;
                    
                    var forward = newLocalTransform.ValueRO.Forward();

                    var physicsVelocity = SystemAPI.GetComponentRW<PhysicsVelocity>(newEntity);
                    
                    physicsVelocity.ValueRW.Linear = new float3(forward.x , forward.y ,forward.z) * bulletSpawner.ValueRW.bulletSpeed;
                }
            }
        }
    }
}