using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public static KeyScript instance;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int LoadKeyValue(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        return 0;
    }

    public void SaveKeyValue(int value, string key)
    {
        PlayerPrefs.SetInt(key, value);
    }
}
