using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SongInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public AudioClip song;
    public float bpm;
    public float offset;

    public string songName;

    [System.Serializable]
    public struct NoteInfo
    {
        public Vector3 spawnPos;
        public float time;
        public bool isLeft;
        public bool isSpecial;

        public NoteInfo(Vector3 spawnPos, float time, bool isLeft, bool isSpecial)
        {
            this.spawnPos = spawnPos;
            this.time = time;
            this.isLeft = isLeft;
            this.isSpecial = isSpecial;
        }
    }

    public List<NoteInfo> noteInfos = new();
}