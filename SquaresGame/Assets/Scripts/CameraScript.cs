using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CameraScript : MonoBehaviour
{
    private float rotationSpeed = 90.0f;

    public GameObject CubeBox;
    private Vector3 rotationPoint;

    public bool isR = false;
    public bool isL = false;
    public bool isU = false;
    public bool isD = false;

    // Use this for initialization
    void Start()
    {
        rotationPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isU)
        {
            RotateU();
        }
        else if (isD)
        {
            RotateD();
        }

        if (isR)
        {
            RotateR();
        }
        else if (isL)
        {
            RotateL();
        }
    }

    private void RotateR()
    {
        CubeBox.transform.RotateAround(rotationPoint, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }

    private void RotateL()
    {
        CubeBox.transform.RotateAround(rotationPoint, new Vector3(0, -1, 0), rotationSpeed * Time.deltaTime);
    }

    private void RotateU()
    {
        CubeBox.transform.RotateAround(rotationPoint, new Vector3(-1, 0, 0), rotationSpeed * Time.deltaTime);
    }

    public void RotateD()
    {
        CubeBox.transform.RotateAround(rotationPoint, new Vector3(1, 0, 0), rotationSpeed * Time.deltaTime);
    }

    private void onPointerDownU()
    {
        isU = true;
    }

    public void onPointerUpU()
    {
        isU = false;
    }

    public void onPointerDownD()
    {
        isD = true;
    }

    public void onPointerUpD()
    {
        isD = false;
    }

    public void onPointerDownR()
    {
        isR = true;
    }

    public void onPointerUpR()
    {
        isR = false;
    }

    public void onPointerDownL()
    {
        isL = true;
    }

    public void onPointerUpL()
    {
        isL = false;
    }
}
