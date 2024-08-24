using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace PiuPiu.Scripts.Ecs.Character
{
    public class CoinAuthoring : MonoBehaviour
    {
        public int countUp = 1;
        
        class Baker : Baker<CoinAuthoring>
        {
            public override void Bake(CoinAuthoring authoring)
            {
                // The entity will be moved
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CoinUpData
                {
                    countUp = authoring.countUp,
                });
            }
        }
    }
}