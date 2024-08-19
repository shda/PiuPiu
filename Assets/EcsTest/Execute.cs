using Unity.Entities;
using UnityEngine;

namespace EcsTest
{
    public class Execute : MonoBehaviour
    {
        class Baker : Baker<Execute>
        {
            public override void Bake(Execute authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<ExecuteMainThread>(entity);
            }
        }
        
    }
    
    public struct ExecuteMainThread : IComponentData
    {
    }
}