using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace PiuPiu.Scripts.Ecs.Player
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct InputSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ref var inputState = ref SystemAPI.GetSingletonRW<InputData>().ValueRW;
            inputState.Horizontal = Input.GetAxisRaw("Horizontal");
            inputState.Vertical = Input.GetAxisRaw("Vertical");
            inputState.MouseX = Input.GetAxisRaw("Mouse X");
            inputState.MouseY = Input.GetAxisRaw("Mouse Y");
            inputState.Space = Input.GetKey(KeyCode.Space);
        }
    }
}