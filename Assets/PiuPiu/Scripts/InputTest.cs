using UnityEngine;
using UnityEngine.InputSystem;

namespace PiuPiu.Scripts
{
    public class InputTest : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void FixedUpdate()
        {
            var gamepad = Gamepad.current;
            if (gamepad == null)
                return; // No gamepad connected.

            if (gamepad.rightTrigger.isPressed)
            {
                Debug.Log("rightTrigger");
                // 'Use' code here
            }

            Vector2 move = gamepad.leftStick.ReadValue();
            Debug.Log(move);
            // 'Move' code here
        }
    }
}
