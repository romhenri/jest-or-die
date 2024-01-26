using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private struct InputConstants
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string JUMP = "Jump";
        public const string CROUCH = "Crouch";
    }
    public Vector2 GetMovementInput()
    {
        // Input Telado
        float horizontalInput = Input.GetAxisRaw(InputConstants.HORIZONTAL);
        // log
        //Debug.Log("Horizontal Input: " + horizontalInput);

        return new Vector2(horizontalInput, 0);
    }

    public bool isJumpButtonDown()
    {
        bool isKeyDownButtonDown = Input.GetKeyDown(KeyCode.Space);
        return isKeyDownButtonDown;
    }

    public bool isJumpButtonHeld()
    {
        bool isKeyDownButtonHeld = Input.GetKey(KeyCode.Space);
        return isKeyDownButtonHeld;
    }

    public bool isCrouchButtonDown()
    {
        bool isKeyDownButtonDown = Input.GetKey(KeyCode.S);
        return isKeyDownButtonDown;
    }

    public bool isCrouchButtonUp()
    {
        bool isKeyDownButtonUp = Input.GetKey(KeyCode.S) == false;
        return isKeyDownButtonUp;
    }
}
