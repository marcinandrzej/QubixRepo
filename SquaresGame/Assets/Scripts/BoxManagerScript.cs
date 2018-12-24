using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManagerScript : MonoBehaviour
{
    public static BoxManagerScript instance;
    private const float DIST = 1.0f;

    private List<GameObject> boxInGame;
    private BoxStrategyScript boxStrategy;

    public List<GameObject> BoxInGame
    {
        get
        {
            return boxInGame;
        }

        set
        {
            boxInGame = value;
        }
    }

    // Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;
    }

    public void SetUpScene(int dimension, int colorsNumber, Color32[] colors, BoxStrategyScript strategy, Material mat)
    {
        boxStrategy = strategy;
        BoxInGame = boxStrategy.SetUpScene(transform, dimension, colorsNumber, colors, DIST, mat);
    }

    public GameObject PlaceBox(Vector3 hitPoint, BoxScript box, int colorIndex, Color32 color)
    {
        GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ob.transform.SetParent(transform);
        ob.AddComponent<BoxScript>();
        ob.GetComponent<BoxScript>().SetBox(colorIndex, color);

        float distance = float.MaxValue;
        Vector3 posit = new Vector3(0, 0, 0);

        for (int i = 0; i < 6; i++)
        {
            if (Vector3.Distance(hitPoint, box.SpawnPoints[i].transform.position) < distance)
            {
                distance = Vector3.Distance(hitPoint, box.SpawnPoints[i].transform.position);
                posit = box.SpawnPoints[i].transform.position;
            }
        }
        ob.transform.position = posit;
        ob.transform.localRotation = Quaternion.Euler(0, 0, 0);
        BoxInGame.Add(ob);
        return ob;
    }

    public List<int> GetColorsInPlay()
    {
        List<int> col = new List<int>();
        for (int i = 0; i < boxInGame.Count; i++)
        {
            int colInd = boxInGame[i].GetComponent<BoxScript>().Color;
            if (!col.Contains(colInd))
                col.Add(colInd);
        }
        return col;
    }

    public void DestroyAll()
    {
        foreach (GameObject gO in boxInGame)
        {
            DestroyObject(gO);
        }
    }
}
