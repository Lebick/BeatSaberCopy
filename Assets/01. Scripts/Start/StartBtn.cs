using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public int currentIndex;

    public List<GameObject> songUIs = new();
    public List<GameInfo> songInfos = new();

    private bool isStart;
    public GameObject startUI;
    public GameObject nextBtn;

    public CanvasGroup canvasGroup;
    public Transform box;

    public CreateNoteInfo test;
    public NoteSpawn test2;

    public void OnClickSelectBtn()
    {
        if (!isStart)
        {
            startUI.SetActive(false);
            isStart = true;
            songUIs[0].SetActive(true);
            nextBtn.SetActive(true);
        }
        else
        {
            GamePlayManager.instance.gameInfo = songInfos[currentIndex];
            canvasGroup.interactable = false;
            StartCoroutine(GameStart());
        }
    }

    public void OnClickNextBtn(int value)
    {
        currentIndex += value;

        if (currentIndex >= songUIs.Count) // 2 >= 3
            currentIndex = 0;

        if (currentIndex < 0)
            currentIndex = songUIs.Count - 1;

        for(int i=0; i<songUIs.Count; i++)
            songUIs[i].SetActive(false);

        songUIs[currentIndex].SetActive(true);
    }

    private IEnumerator GameStart()
    {
        float progress = 0f;

        while(progress <= 1f)
        {
            progress += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(1, 0, progress);

            yield return null;
        }

        progress = 0f;

        Vector3 start = box.localScale;

        while(progress <= 2f)
        {
            progress += Time.deltaTime;

            box.localScale = Vector3.Lerp(start, new Vector3(10, 10, 10), progress / 2f);

            yield return null;
        }

        //test.SetSong();
        test2.SetSong();

        canvasGroup.gameObject.SetActive(false);
    }
}
