using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteSpawn : MonoBehaviour
{
    private GameInfo info;

    public float songProgress;

    private bool isStart;

    public GameObject leftNotePrefab;
    public GameObject leftSpecialNotePrefab;
    public GameObject leftEffectPrefab;
    public GameObject rightNotePrefab;
    public GameObject rightSpecialNotePrefab;
    public GameObject rightEffectPrefab;

    public HashSet<GameInfo.NoteInfo> useNotes = new(); // HashSet으로 성능 개선

    public Queue<GameObject> leftPullingNote = new();
    public Queue<GameObject> leftSpecialPullingNote = new();
    public Queue<GameObject> rightPullingNote = new();
    public Queue<GameObject> rightSpecialPullingNote = new();

    public Queue<GameObject> leftPullingEffect = new();
    public Queue<GameObject> rightPullingEffect = new();

    private void Start()
    {
        info = GamePlayManager.instance.gameInfo;
    }

    public void SetSong()
    {
        info = GamePlayManager.instance.gameInfo;
        StartCoroutine(StartSong());
    }

    private void Update()
    {
        if (isStart)
        {
            songProgress += Time.deltaTime;

            foreach (var note in info.noteInfos)
            {
                if (note.time <= songProgress && !useNotes.Contains(note))
                {
                    useNotes.Add(note);

                    if (note.isSpecial)
                    {
                        if (note.isLeft)
                            GetLeftSpecialNote(note);
                        else
                            GetRightSpecialNote(note);
                    }
                    else
                    {
                        if (note.isLeft)
                            GetLeftNote(note);
                        else
                            GetRightNote(note);
                    }
                }
            }

            CheckGameEnd();
        }
    }

    private void GetLeftSpecialNote(GameInfo.NoteInfo note)
    {
        if (leftSpecialPullingNote.Count == 0)
        {
            GameObject newNote = Instantiate(leftSpecialNotePrefab, note.spawnPos, Quaternion.identity);
            newNote.GetComponent<Note>().noteSpawn = this;
        }
        else
        {
            GameObject noteObj = leftSpecialPullingNote.Dequeue();
            Vector3 pos = note.spawnPos;
            pos.z = 17;
            noteObj.transform.position = pos;
            noteObj.SetActive(true);
            noteObj.GetComponent<Note>().initialize();
        }
    }

    private void GetRightSpecialNote(GameInfo.NoteInfo note)
    {
        if (rightSpecialPullingNote.Count == 0)
        {
            GameObject newNote = Instantiate(rightSpecialNotePrefab, note.spawnPos, Quaternion.identity);
            newNote.GetComponent<Note>().noteSpawn = this;
        }
        else
        {
            GameObject noteObj = rightSpecialPullingNote.Dequeue();
            Vector3 pos = note.spawnPos;
            pos.z = 17;
            noteObj.transform.position = pos;
            noteObj.SetActive(true);
            noteObj.GetComponent<Note>().initialize();
        }
    }

    private void GetLeftNote(GameInfo.NoteInfo note)
    {
        if (leftPullingNote.Count == 0)
        {
            GameObject newNote = Instantiate(leftNotePrefab, note.spawnPos, Quaternion.identity);
            newNote.GetComponent<Note>().noteSpawn = this;
        }
        else
        {
            GameObject noteObj = leftPullingNote.Dequeue();
            Vector3 pos = note.spawnPos;
            pos.z = 17;
            noteObj.transform.position = pos;
            noteObj.SetActive(true);
            noteObj.GetComponent<Note>().initialize();
        }
    }

    private void GetRightNote(GameInfo.NoteInfo note)
    {
        if (rightPullingNote.Count == 0)
        {
            GameObject newNote = Instantiate(rightNotePrefab, note.spawnPos, Quaternion.identity);
            newNote.GetComponent<Note>().noteSpawn = this;
        }
        else
        {
            GameObject noteObj = rightPullingNote.Dequeue();
            Vector3 pos = note.spawnPos;
            pos.z = 17;
            noteObj.transform.position = pos;
            noteObj.SetActive(true);
            noteObj.GetComponent<Note>().initialize();
        }
    }

    public void GetLeftEffect(Vector3 pos)
    {
        if (leftPullingEffect.Count == 0)
        {
            GameObject newEffect = Instantiate(leftEffectPrefab, pos, Quaternion.identity);
            newEffect.GetComponent<PullingEffect>().noteSpawn = this;
            newEffect.GetComponent<PullingEffect>().isLeft = true;
        }
        else
        {
            GameObject effectObj = leftPullingEffect.Dequeue();
            effectObj.transform.position = pos;
            effectObj.SetActive(true);
        }
    }

    public void GetRightEffect(Vector3 pos)
    {
        if (rightPullingEffect.Count == 0)
        {
            GameObject newEffect = Instantiate(rightEffectPrefab, pos, Quaternion.identity);
            newEffect.GetComponent<PullingEffect>().noteSpawn = this;
            newEffect.GetComponent<PullingEffect>().isLeft = false;
        }
        else
        {
            GameObject effectObj = rightPullingEffect.Dequeue();
            effectObj.transform.position = pos;
            effectObj.SetActive(true);
        }
    }

    private IEnumerator StartSong()
    {
        float progress = 0f;

        while (progress <= info.offset)
        {
            progress += Time.deltaTime;
            yield return null;
        }

        isStart = true;
        songProgress += progress - info.offset;

        progress -= info.offset;

        while (progress <= 1f)
        {
            progress += Time.deltaTime;
            yield return null;
        }

        AudioManager.instance.PlayMusic(info.song, progress - 1f);
    }

    private bool isEnd;

    private void CheckGameEnd()
    {
        if(info.noteInfos.Count == useNotes.Count && !isEnd)
        {
            isEnd = true;
            StartCoroutine(WaitEnd());
        }
    }


    private IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(2f);
        GamePlayManager.instance.GameEnd();
    }

}