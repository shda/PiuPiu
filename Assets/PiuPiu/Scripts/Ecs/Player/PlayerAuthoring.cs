using PiuPiu.Scripts.Ecs.Character;
using PiuPiu.Scripts.Ecs.Coin;
using PiuPiu.Scripts.Ecs.EatThings;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Player
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health = 100;
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovingData
                {
                    MovingDirection = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });

                AddComponent(entity , new HealthData
                {
                    health = authoring.health,
                    maxHealth = authoring.maxHealth,
                });
                
                AddComponent(entity , new PlayerData());
                AddComponent(entity , new InputData());
                AddComponent(entity , new PlayerRotateMouseData());
                AddComponent(entity , new PlayerCameraData());
                AddComponent(entity , new IsCanEatThingsTag());
                AddComponent(entity , new CoinCountData());
            }
        }
    }
}