using PiuPiu.Scripts.Ecs.Character.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private bool isPlayerControl;
        [SerializeField] private float speedMoving;
        
        class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterMovingData
                {
                    Direction = new float3(0,0,0),
                    Speed = authoring.speedMoving,
                });
                if (authoring.isPlayerControl)
                {
                   // World.DefaultGameObjectInjectionWorld.EntityManager.SetName(entity , "Player");
                    AddComponent(entity, new PlayerData());
                    AddComponent(entity , new InputData());
                }
            }
        }
    }

    struct Spawner : IComponentData
    {
        public Entity Prefab;
    }
}
