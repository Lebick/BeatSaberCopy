using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NoteType
{
    Normal,
    DontHit,
    Guard
}

[CreateAssetMenu(fileName = "SongInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public AudioClip song;
    public float bpm;
    public float offset;

    [System.Serializable]
    public struct NoteInfo
    {
        public Vector3 spawnPos;
        public float time;
        public NoteType noteType;
        public bool isLeft;

        public NoteInfo(Vector3 spawnPos, float time, NoteType noteType, bool isLeft, bool isSpawn = false)
        {
            this.spawnPos = spawnPos;
            this.time = time;
            this.noteType = noteType;
            this.isLeft = isLeft;
        }
    }

    public List<NoteInfo> noteInfos = new();
}