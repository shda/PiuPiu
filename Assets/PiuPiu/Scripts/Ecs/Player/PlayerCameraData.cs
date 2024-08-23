using Unity.Entities;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Player
{
    public struct PlayerCameraData : IComponentData
    {
        
    }
    
    public class TransformCameraData : IComponentData
    {
        public Transform Transform;
    }
}