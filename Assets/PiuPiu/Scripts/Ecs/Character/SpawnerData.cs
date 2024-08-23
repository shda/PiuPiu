using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    struct SpawnerData : IComponentData
    {
        public Entity Prefab;
    }
}