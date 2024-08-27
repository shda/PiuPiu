using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Player
{
    public struct InputData : IComponentData
    {
        public float Horizontal;
        public float Vertical;
        public float MouseX;
        public float MouseY;
        public bool Space;
    }
}