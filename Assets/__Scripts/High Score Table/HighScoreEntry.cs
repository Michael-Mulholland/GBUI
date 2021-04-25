using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry
{
    // == member variables ==
    public int score;
    public string name;

    // == member methods ==
    public HighScoreEntry(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
}
