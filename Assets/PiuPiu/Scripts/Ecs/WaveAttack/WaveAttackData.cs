using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.Character
{
    public struct WaveAttackData : IComponentData
    {
        public float delayAfterAttack;
        public float downTimeAfterAttack;
        
        public float currentDelayAfterAttack;
    }
}