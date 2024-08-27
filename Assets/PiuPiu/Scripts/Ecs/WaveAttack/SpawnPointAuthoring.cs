using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.WaveAttack
{
    public class SpawnPointAuthoring  : MonoBehaviour
    {
        class Baker : Baker<SpawnPointAuthoring>
        {
            public override void Bake(SpawnPointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
            }
        }
    }
}