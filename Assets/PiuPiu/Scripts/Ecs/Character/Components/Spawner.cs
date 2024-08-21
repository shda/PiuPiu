using Unity.Entities;

namespace PiuPiu.Scripts.Ecs
{
    struct Spawner : IComponentData
    {
        public Entity Prefab;
    }
}