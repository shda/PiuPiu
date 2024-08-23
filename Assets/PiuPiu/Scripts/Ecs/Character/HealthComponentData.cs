using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct HealthComponentData : IComponentData
    {
        public int maxHealth;
        public int currentHealth;
    }
}