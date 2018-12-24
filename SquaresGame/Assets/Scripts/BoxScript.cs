using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private const float RAYCAST_RANGE = 1.2f;
    private const float DIST = 1.0f;
    private int color;
    private GameObject[] spawnPoints;

    public int Color
    {
        get
        {
            return color;
        }

        set
        {
            color = value;
        }
    }

    public GameObject[] SpawnPoints
    {
        get
        {
            return spawnPoints;
        }

        set
        {
            spawnPoints = value;
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    void OnMouseDown()
    {
        BoxManagerScript.instance.Play(this);
    }*/

    public void SetBox(int colorIndex, Color32 colorMaterial)
    {
        color = colorIndex;
        gameObject.GetComponent<MeshRenderer>().material.color = colorMaterial;
        SpawnPoints = new GameObject[6];
        SpawnPoints[0] = new GameObject("SpawnPointX");
        SpawnPoints[0].transform.SetParent(transform);
        SpawnPoints[0].transform.localPosition = new Vector3(DIST, 0, 0);
        SpawnPoints[1] = new GameObject("SpawnPoint-X");
        SpawnPoints[1].transform.SetParent(transform);
        SpawnPoints[1].transform.localPosition = new Vector3(-DIST, 0, 0);
        SpawnPoints[2] = new GameObject("SpawnPointY");
        SpawnPoints[2].transform.SetParent(transform);
        SpawnPoints[2].transform.localPosition = new Vector3(0, DIST, 0);
        SpawnPoints[3] = new GameObject("SpawnPoint-Y");
        SpawnPoints[3].transform.SetParent(transform);
        SpawnPoints[3].transform.localPosition = new Vector3(0, -DIST, 0);
        SpawnPoints[4] = new GameObject("SpawnPointZ");
        SpawnPoints[4].transform.SetParent(transform);
        SpawnPoints[4].transform.localPosition = new Vector3(0, 0, DIST);
        SpawnPoints[5] = new GameObject("SpawnPoint-Z");
        SpawnPoints[5].transform.SetParent(transform);
        SpawnPoints[5].transform.localPosition = new Vector3(0, 0, -DIST);
    }

    public void Check(int color, List<GameObject> toDestroy)
    {
        Vector3 pos = transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(pos, SpawnPoints[5].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if(gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }

        if (Physics.Raycast(pos, SpawnPoints[4].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if (gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }

        if (Physics.Raycast(pos, SpawnPoints[2].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if (gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }

        if (Physics.Raycast(pos, SpawnPoints[3].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if (gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }

        if (Physics.Raycast(pos, SpawnPoints[0].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if (gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }

        if (Physics.Raycast(pos, SpawnPoints[1].transform.position - pos, out hitInfo, RAYCAST_RANGE))
        {
            GameObject gO = hitInfo.transform.gameObject;
            if (gO.GetComponent<BoxScript>().Color == color)
            {
                if (!toDestroy.Contains(gO))
                {
                    toDestroy.Add(gO);
                    gO.GetComponent<BoxScript>().Check(color, toDestroy);
                }
            }
        }
    }
}
