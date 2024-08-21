using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character.Components
{
    public struct HealthComponentData : IComponentData
    {
        public int maxHealth;
        public int currentHealth;
    }
}