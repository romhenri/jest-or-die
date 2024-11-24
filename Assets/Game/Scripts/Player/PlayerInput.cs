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

    public bool isSpinButtonDown()
    {
        bool isSpinButtonDown = Input.GetKeyDown(KeyCode.F);
        return isSpinButtonDown;
    }

    public bool isSpinButtonHeld()
    {
        bool isSpinButtonHeld = Input.GetKey(KeyCode.F);
        return isSpinButtonHeld;
    }

    public bool isCrouchButtonDown()
    {
        bool isKeyDownButtonDown = Input.GetKey(KeyCode.LeftControl);
        return isKeyDownButtonDown;
    }

    public bool isCrouchButtonUp()
    {
        bool isKeyDownButtonUp = Input.GetKey(KeyCode.LeftControl) == false;
        return isKeyDownButtonUp;
    }

    public bool isDescendButtonHeld()
    {
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }
}
