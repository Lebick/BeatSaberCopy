using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SongInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public AudioClip song;
    public float bpm;

    [System.Serializable]
    public struct NoteInfo
    {
        public Vector3 spawnPos;
        public float time;

        public NoteInfo(Vector3 spawnPos, float time)
        {
            this.spawnPos = spawnPos;
            this.time = time;
        }
    }

    public List<NoteInfo> noteInfos = new();
}