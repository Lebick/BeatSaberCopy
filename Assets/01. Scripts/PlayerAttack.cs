using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference aButtonInput;

    private bool isInputRightBtn;
    public float btnReleaseTime;

    private void Update()
    {
        aButtonInput.action.performed += CheckBtnTrigger;
        aButtonInput.action.canceled += CheckBtnRelease;

        if (isInputRightBtn)
            btnReleaseTime += Time.deltaTime;
        else
            btnReleaseTime = 0;
    }

    private void CheckBtnTrigger(InputAction.CallbackContext obj)
    {
        isInputRightBtn = true;
    }

    private void CheckBtnRelease(InputAction.CallbackContext obj)
    {
        isInputRightBtn = false;
    }
}
