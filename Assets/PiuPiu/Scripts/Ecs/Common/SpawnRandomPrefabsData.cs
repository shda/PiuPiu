using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct SpawnRandomPrefabsData : IComponentData
    {
        public int Count;
        public float3 offset;
        public float factor;

        public bool isAlreadySpawn;
    }
    
    [Serializable]
    [InternalBufferCapacity(8)]
    public struct SpawnPrefab : IBufferElementData
    {
        public Entity Prefab;
    }
}