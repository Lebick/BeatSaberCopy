using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CreateNoteInfo : MonoBehaviour
{
    private GameInfo info;

    public bool isStart;

    public InputActionReference leftButtonInput;
    public InputActionReference leftSpecialButtonInput;
    public InputActionReference rightButtonInput;
    public InputActionReference rightSpecialButtonInput;

    public Transform leftController;
    public Transform rightController;

    private bool isLeftInput;
    private bool isLeftSpecialInput;
    private bool isRightInput;
    private bool isRightSpecialInput;

    public float songProgress;

    private void Update()
    {
        if (isStart)
        {
            songProgress += Time.deltaTime;

            leftButtonInput.action.performed += CheckLeftBtnTrigger;

            rightButtonInput.action.performed += CheckRightBtnTrigger;

            leftSpecialButtonInput.action.performed += CheckLeftSpecialBtnTrigger;

            rightSpecialButtonInput.action.performed += CheckRightSpecialBtnTrigger;

            if (isLeftInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetLeftRayPosition(), songProgress, true, false));
            }

            if (isRightInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetRightRayPosition(), songProgress, false, false));
            }

            if (isLeftSpecialInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetLeftRayPosition(), songProgress, true, true));
            }

            if (isRightSpecialInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetRightRayPosition(), songProgress, false, true));
            }
        }

        isLeftInput = false;
        isRightInput = false;
        isLeftSpecialInput = false;
        isRightSpecialInput = false;
    }

    private Vector3 GetLeftRayPosition()
    {
        Ray ray = new Ray(leftController.position, leftController.forward);

        Physics.Raycast(ray, out RaycastHit hit, 50f, LayerMask.GetMask("Wall"));

        return hit.point;
    }

    private Vector3 GetRightRayPosition()
    {
        Ray ray = new Ray(rightController.position, rightController.forward);

        Physics.Raycast(ray, out RaycastHit hit, 50f, LayerMask.GetMask("Wall"));

        return hit.point;
    }

    #region 버튼 정보
    private void CheckLeftBtnTrigger(InputAction.CallbackContext obj)
    {
        isLeftInput = true;
    }

    private void CheckRightBtnTrigger(InputAction.CallbackContext obj)
    {
        isRightInput = true;
    }

    private void CheckLeftSpecialBtnTrigger(InputAction.CallbackContext obj)
    {
        isLeftSpecialInput = true;
    }

    private void CheckRightSpecialBtnTrigger(InputAction.CallbackContext obj)
    {
        isRightSpecialInput = true;
    }

    #endregion

    private void Start()
    {
        info = GamePlayManager.instance.gameInfo;
        SetSong();
    }

    public void SetSong()
    {
        StartCoroutine(StartSong());
    }


    private IEnumerator StartSong()
    {
        yield return new WaitForSeconds(2);
        print("1");
        yield return new WaitForSeconds(1);

        float progress = 0f;

        while(progress <= info.offset)
        {
            progress += Time.deltaTime;

            yield return null;
        }

        AudioManager.instance.PlayMusic(info.song, progress - info.offset);
        songProgress += progress - info.offset;
        isStart = true;
    }
}
