using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreList
{
    // == member variables == 
    public List<HighScoreEntry> scoreList;

    // == gets/sets ==
    public HighScoreList(List<HighScoreEntry> scoreList)
    {
        this.scoreList = scoreList;
    }

    public List<HighScoreEntry> getHighScoreList()
    {
        return this.scoreList;
    }
}
