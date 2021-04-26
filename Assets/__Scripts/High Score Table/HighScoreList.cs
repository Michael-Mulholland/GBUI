using System.Collections.Generic;

[System.Serializable]
public class HighScoreList
{
    // public fields
    public List<HighScoreEntry> scoreList;

    // getter
    public HighScoreList(List<HighScoreEntry> scoreList)
    {
        this.scoreList = scoreList;
    }

    public List<HighScoreEntry> getHighScoreList()
    {
        return this.scoreList;
    }
}
