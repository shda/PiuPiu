using PiuPiu.Scripts.Ecs.Character.Components;
using PiuPiu.Scripts.Ecs.Character.Systems;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterMovingData
                {
                    MovingDirection = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });
                
                AddComponent(entity , new PlayerData());
                AddComponent(entity , new InputData());
                AddComponent(entity , new PlayerRotateMouseData());
            }
        }
    }
}