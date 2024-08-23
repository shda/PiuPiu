using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
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