using PiuPiu.Scripts.Ecs.Character.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Character.Components
{
    public struct PlayerCameraData : IComponentData
    {
        
    }
    
    public class TransformCameraData : IComponentData
    {
        public Transform Transform;
    }
}