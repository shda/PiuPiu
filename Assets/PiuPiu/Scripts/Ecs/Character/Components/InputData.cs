using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Components
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