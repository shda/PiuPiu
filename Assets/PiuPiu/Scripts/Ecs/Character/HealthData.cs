using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct HealthData : IComponentData
    {
        public int maxHealth;
        public int health;
    }
}