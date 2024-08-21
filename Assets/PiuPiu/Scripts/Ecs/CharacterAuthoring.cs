using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterMovingData
                {
                    MovingDirection = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });
            }
        }
    }
}
