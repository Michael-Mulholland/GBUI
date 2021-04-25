using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // == member variables ==
    public int score;

    // == member methods ==
    public SaveData(int data)
    {
        score = data;
    }
}
