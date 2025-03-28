using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference aButtonInput;

    private bool isInputRightBtn;
    public float btnReleaseTime;

    public bool isLeft;

    public Transform speedDetect;

    public Vector3 lastPos;

    public float distance;

    private void Start()
    {
        lastPos = speedDetect.position;
    }

    private void Update()
    {
        aButtonInput.action.performed += CheckBtnTrigger;
        aButtonInput.action.canceled += CheckBtnRelease;

        if (isInputRightBtn)
            btnReleaseTime += Time.deltaTime;
        else
            btnReleaseTime = 0;
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(lastPos, speedDetect.position);

        lastPos = speedDetect.position;
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
