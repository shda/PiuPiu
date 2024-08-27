using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.EatThings
{
    public class EatThingAuthoring  : MonoBehaviour
    {
        public int countUpHealth = 10;
        class Baker : Baker<EatThingAuthoring>
        {
            public override void Bake(EatThingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity , new EatThingData
                {
                    upHealth = authoring.countUpHealth,
                });
            }
        }
    }
}