using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteSpawn : MonoBehaviour
{
    public GameInfo info;

    public float songProgress;

    private bool isStart;

    public GameObject leftNotePrefab;
    public GameObject leftEffectPrefab;
    public GameObject rightNotePrefab;
    public GameObject rightEffectPrefab;

    public HashSet<GameInfo.NoteInfo> useNotes = new(); // HashSet으로 성능 개선

    public Queue<GameObject> leftPullingNote = new(); // Queue로 오브젝트 관리
    public Queue<GameObject> rightPullingNote = new();

    public Queue<GameObject> leftPullingEffect = new();
    public Queue<GameObject> rightPullingEffect = new();

    public void SetSong()
    {
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
                    if (note.isLeft)
                        GetLeftNote(note);
                    else
                        GetRightNote(note);
                }
            }
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
}