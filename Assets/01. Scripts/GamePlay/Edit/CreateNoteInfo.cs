using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CreateNoteInfo : MonoBehaviour
{
    public GameInfo info;

    public bool isStart;

    public InputActionReference leftButtonInput;
    public InputActionReference rightButtonInput;

    public Transform leftController;
    public Transform rightController;

    private bool isLeftInput;
    private bool isRightInput;

    public float songProgress;

    private void Update()
    {
        if (isStart)
        {
            songProgress += Time.deltaTime;

            leftButtonInput.action.performed += CheckLeftBtnTrigger;

            rightButtonInput.action.performed += CheckRightBtnTrigger;

            if (isLeftInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetLeftRayPosition(), songProgress, NoteType.Normal, true));
            }

            if (isRightInput)
            {
                info.noteInfos.Add(new GameInfo.NoteInfo(GetRightRayPosition(), songProgress, NoteType.Normal, false));
            }
        }

        isLeftInput = false;
        isRightInput = false;
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

    #endregion

    private void Start()
    {
        SetSong();
    }

    public void SetSong()
    {
        StartCoroutine(StartSong());
    }


    private IEnumerator StartSong()
    {
        yield return new WaitForSeconds(5); 

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
