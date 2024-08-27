using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.WaveAttack
{
    [InternalBufferCapacity(8)]
    public struct SpawnPoints : IBufferElementData
    {
        public Entity Point;
    }
}