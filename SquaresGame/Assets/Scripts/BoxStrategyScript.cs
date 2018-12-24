using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BoxStrategyScript
{
    List<GameObject> SetUpScene(Transform parent, int dimension, int colorsNumber, Color32[] colors, float dist, Material mat);
}

public class CubeStrategy : BoxStrategyScript
{
    public List<GameObject> SetUpScene(Transform parent, int dimension, int colorsNumber, Color32[] colors, float dist, Material mat)
    {
        List<GameObject> boxInGame = new List<GameObject>();

        int centre = -dimension / 2;
        for (int x = centre; x < dimension + centre; x++)
        {
            for (int y = centre; y < dimension + centre; y++)
            {
                for (int z = centre; z < dimension + centre; z++)
                {
                    int index = Random.Range(0, colorsNumber);
                    GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ob.GetComponent<Renderer>().material = mat;
                    ob.transform.SetParent(parent);
                    ob.transform.position = new Vector3(x * dist, y * dist, z * dist);
                    ob.AddComponent<BoxScript>();
                    ob.GetComponent<BoxScript>().SetBox(index, colors[index]);
                    boxInGame.Add(ob);
                }
            }
        }
        return boxInGame;
    }
}

public class PyramidStrategy : BoxStrategyScript
{
    public List<GameObject> SetUpScene(Transform parent, int dimension, int colorsNumber, Color32[] colors, float dist, Material mat)
    {
        List<GameObject> boxInGame = new List<GameObject>();

        int centre = -dimension / 2;
        int dimXZ = ((dimension * 2) - 1);
        int centreXZ = -((dimension * 2) - 1)/2;
        int offset = 0;
        for (int y = centre ; y < dimension + centre; y++)
        {
            for (int x = centreXZ + offset; x < dimXZ + centreXZ - offset; x++)
            {
                for (int z = centreXZ + offset; z < dimXZ + centreXZ - offset; z++)
                {
                    int index = Random.Range(0, colorsNumber);
                    GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ob.GetComponent<Renderer>().material = mat;
                    ob.transform.SetParent(parent);
                    ob.transform.position = new Vector3(x * dist, y * dist, z * dist);
                    ob.AddComponent<BoxScript>();
                    ob.GetComponent<BoxScript>().SetBox(index, colors[index]);
                    boxInGame.Add(ob);
                }
            }
            offset++;
        }
        return boxInGame;
    }
}

public class DiamndStrategy : BoxStrategyScript
{
    public List<GameObject> SetUpScene(Transform parent, int dimension, int colorsNumber, Color32[] colors, float dist, Material mat)
    {
        List<GameObject> boxInGame = new List<GameObject>();

        int centre = -dimension / 2;
        int offset = - centre - 1;
        for (int y = centre; y < dimension + centre; y++)
        {
            for (int x = centre + offset; x < dimension + centre - offset; x++)
            {
                for (int z = centre + offset; z < dimension + centre - offset; z++)
                {
                    int index = Random.Range(0, colorsNumber);
                    GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ob.GetComponent<Renderer>().material = mat;
                    ob.transform.SetParent(parent);
                    ob.transform.position = new Vector3(x * dist, y * dist, z * dist);
                    ob.AddComponent<BoxScript>();
                    ob.GetComponent<BoxScript>().SetBox(index, colors[index]);
                    boxInGame.Add(ob);
                }
            }
            if (y < 0)
            {
                offset--;
            }
            else
            {
                offset++;
            }
        }
        return boxInGame;
    }
}

