using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Burst;
using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Player
{
    public partial struct PlayerFireSystem : ISystem
    {
        [BurstCompile] 
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputData>(); 
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //InputData должен быть в оденочном экземпляре и только у одного Entity
            var input = SystemAPI.GetSingleton<InputData>();

            foreach (var bulletSpawner  in SystemAPI.Query<RefRW<BulletSpawnerData>>())
            {
                bulletSpawner.ValueRW.isFireing = input.Space;
            }
        }
    }
}