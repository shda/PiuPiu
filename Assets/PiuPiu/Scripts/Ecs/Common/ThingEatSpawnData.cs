using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct EatThingSpawnData : IComponentData
    {
        public Entity Prefab;
    }
}