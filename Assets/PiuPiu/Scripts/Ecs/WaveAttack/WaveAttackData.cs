using Unity.Entities;

namespace PiuPiu.Scripts.Ecs.WaveAttack
{
    public struct WaveAttackData : IComponentData
    {
        public float delayAfterAttack;
        public float downTimeAfterAttack;
        
        public float currentDelayAfterAttack;
    }
}