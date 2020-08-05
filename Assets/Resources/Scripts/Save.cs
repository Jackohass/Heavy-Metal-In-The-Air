using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    [System.Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
    }
    public List<ScoreEntry> scoreList = new List<ScoreEntry>();
}
