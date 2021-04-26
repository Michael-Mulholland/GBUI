using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry
{
    // public fields
    public int score;
    public string name;
    public HighScoreEntry(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
}