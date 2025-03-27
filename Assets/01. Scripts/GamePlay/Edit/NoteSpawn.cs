using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteSpawn : MonoBehaviour
{
    public GameInfo info;

    public float songProgress;

    private bool isStart;

    public GameObject notePrefab;

    public List<GameInfo.NoteInfo> useNotes = new();

    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private void Update()
    {

        if (isStart)
        {
            songProgress += Time.deltaTime;

            for(int i=0; i<info.noteInfos.Count; i++)
            {
                GameInfo.NoteInfo note = info.noteInfos[i];

                if (note.time <= songProgress && !useNotes.Contains(note))
                {
                    useNotes.Add(note);
                    Instantiate(notePrefab, note.spawnPos, Quaternion.identity);
                }
            }
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

        while(progress <= 1f)
        {
            progress += Time.deltaTime;
            yield return null;
        }

        
        AudioManager.instance.PlayMusic(info.song, progress - 1f);
    }
}
