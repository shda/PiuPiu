using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    [InternalBufferCapacity(8)]
    public struct SpawnPoints : IBufferElementData
    {
        public Entity Point;
    }
}