using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.EatThings
{
    public struct EatThingSpawnData : IComponentData
    {
        public Entity Prefab;
    }
}