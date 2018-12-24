using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public static TimeScript instance;
    private DateTime startTime;
    private TimeSpan time;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartClock()
    {
        startTime = DateTime.Now;
    }

    public void UpdateTime()
    {
        time = DateTime.Now - startTime;
    }

    public int GetTime()
    {
        return (int)time.TotalMilliseconds;
    }

    public string RefactorTime(int t)
    {
        string str = "";
        int tim = t;
        int temp = (tim / 60000);
        if (temp < 10)
        {
            str += "0";
        }
        str += temp.ToString() + ":";
        tim = t % 60000;
        temp = (tim / 1000);
        if (temp < 10)
        {
            str += "0";
        }
        str += temp.ToString() + ":";
        tim = t % 1000;
        temp = tim / 10;
        if (temp < 10)
        {
            str += "0";
        }
        str += temp.ToString();
        return str;
    }
}
