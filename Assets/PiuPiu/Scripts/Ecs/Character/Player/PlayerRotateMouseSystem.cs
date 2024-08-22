using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Ray = UnityEngine.Ray;
using RaycastHit = Unity.Physics.RaycastHit;

namespace PiuPiu.Scripts.Ecs.Character.Player
{
    public partial struct PlayerRotateMouseSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerRotateMouseData>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            EntityQueryBuilder builder = new EntityQueryBuilder(Allocator.Temp).WithAll<PhysicsWorldSingleton>();
            EntityQuery singletonQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(builder);
            var collisionWorld = singletonQuery.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
            singletonQuery.Dispose();

            var r = ray.direction * 1000f;

            RaycastInput input = new RaycastInput()
            {
                Start = new float3(ray.origin.x , ray.origin.y , ray.origin.z),
                End = new float3(r.x , r.y , r.z),
                Filter = new CollisionFilter
                {
                    BelongsTo = ~0u,
                    CollidesWith = ~0u, // all 1s, so all layers, collide with everything
                    GroupIndex = 0
                }
            };

            RaycastHit hit = new RaycastHit();
            bool haveHit = collisionWorld.CastRay(input, out hit);
            if (haveHit)
            {
                foreach (var (movingData , entity) in 
                         SystemAPI.Query<RefRW<CharacterMovingData>>().WithAny<PlayerData>().WithEntityAccess())
                {
                    movingData.ValueRW.RotatePoint = hit.Position;
                }
                
                PointDraw.Instance.transform.position = hit.Position;
            }
        }
    }
    
    public struct PlayerRotateMouseData : IComponentData
    {
    
    }
}