using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(int score)
    {
        // instantiate binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // set string path
        string path = Application.persistentDataPath + "/game.save";
        // instantiate file stream
        FileStream stream = new FileStream(path, FileMode.Create);
        // instantiate player data
        SaveData data = new SaveData(score);
        // serialize data
        formatter.Serialize(stream, data);
        //close stream
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.save";

        if(File.Exists(path))
        {
            // instantiate binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            // instantiate file stream 
            FileStream stream = new FileStream(path, FileMode.Open);
            // instantiate player data
            SaveData data = formatter.Deserialize(stream) as SaveData;
            // close stream
            stream.Close();
            // return data
            return data;
        }
        else
        {
            // file not found 
            return null;
        }
    }

    public static void SaveHighScores(List<HighScoreEntry> scoreList)
    {
        // instantiate binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // set string path
        string path = Application.persistentDataPath + "/highscores.save";
        // instantiate file stream
        FileStream stream = new FileStream(path, FileMode.Create);
        // instantiate high score data 
        HighScoreList highScore = new HighScoreList(scoreList);
        // serialize data
        formatter.Serialize(stream, highScore);
        //close stream
        stream.Close();
    }

    public static HighScoreList LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.save";
        
        if(File.Exists(path))
        {
            // instantiate binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            // instantiate file stream 
            FileStream stream = new FileStream(path, FileMode.Open);
            // instantiate score data
            HighScoreList highScore = formatter.Deserialize(stream) as HighScoreList;
            // close stream
            stream.Close();
            // return data
            return highScore;
        }
        else
        {
            // file not found 
            return null;
        }
    }

    public static void AddHighScoreEntry(int score, string name)
    {
        // create new high score
        HighScoreEntry highScoreEntry = new HighScoreEntry(score, name);
        //load list of previous scores
        HighScoreList highScore = LoadHighScores();
        // add new high score to the list
        highScore.scoreList.Add(highScoreEntry);
        // save modified high score list
        SaveHighScores(highScore.scoreList);
    }
}
