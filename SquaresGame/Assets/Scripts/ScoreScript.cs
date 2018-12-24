using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CountScore(int x)
    {
        int score = 0;
        for (int i = 1; i <= x; i++)
        {
            score += ((i * 10) - 5);
        }
        return score;
    }

    public bool IsNewBest(int score, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) >= score)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsNewBestTime(int score, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) < score)
            {
                return false;
            }
        }
        return true;
    }
}
