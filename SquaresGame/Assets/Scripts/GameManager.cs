using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static int BOX_COLORS = 5;
    private static int GAME_MAX_TIME = 300000;
    public GameObject destroyPrefab;
    public Material cubeMat;

    private Vector3 camPosNear;
    private Vector3 camPosFar;

    private bool isEnabled;
    private bool end;
    private int color;
    private Color32[] colors;

    private int colorNumber;
    private BoxStrategyScript strategy;
    private int size;
    private int score;
    private int target;
    private string key;
    private int time;
    private int bestTime;

    void Awake()
    {
        colors = new Color32[BOX_COLORS];

        colors[0] = new Color32(255, 0, 0, 255);
        colors[1] = new Color32(0, 255, 0, 255);
        colors[2] = new Color32(0, 255, 255, 255);
        colors[3] = new Color32(255, 0, 255, 255);
        colors[4] = new Color32(255, 255, 0, 255);
    }

    // Use this for initialization
    void Start ()
    {
        camPosNear = new Vector3(0, 0, -10);
        camPosFar = new Vector3(0, 0, -15);
    }

    public void SetGame()
    {
        end = false;
        score = 0;
        time = 0;
        ChangeStrategy();
        ChangeSize();
        ChangeColorsCount();
        key = strategy.ToString() + size.ToString() + colorNumber.ToString();
        target = KeyScript.instance.LoadKeyValue(key);
        bestTime = KeyScript.instance.LoadKeyValue(key+"Time");
        if (bestTime == 0)
        {
            bestTime = GAME_MAX_TIME;
        }
        color = Random.Range(0, colorNumber);
        BoxManagerScript.instance.transform.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        BoxManagerScript.instance.SetUpScene(size, colorNumber, colors, strategy, cubeMat);
        GuiScript.instance.ShowMainMenu(false);
        GuiScript.instance.ShowControlPanel(true);
        GuiScript.instance.UpdateColor(colors[color]);
        GuiScript.instance.SetTarget(target);
        GuiScript.instance.SetScore(score);
        GuiScript.instance.SetBestTime(TimeScript.instance.RefactorTime(bestTime));
        GuiScript.instance.SetTime(TimeScript.instance.RefactorTime(0));
        isEnabled = true;
        TimeScript.instance.StartClock();
        StartCoroutine(TimeCoroutine());

    }

    private IEnumerator TimeCoroutine()
    {
        while (!end)
        {
            TimeScript.instance.UpdateTime();
            time = TimeScript.instance.GetTime();
            GuiScript.instance.SetTime(TimeScript.instance.RefactorTime(time));
            GuiScript.instance.SetTimeImg(time, GAME_MAX_TIME);
            if (time > GAME_MAX_TIME)
            {
                End();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void RestartGame()
    {
        GuiScript.instance.ShowEndMenu(false);
        BoxManagerScript.instance.DestroyAll();
        SetGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !end)
        {
            if (isEnabled && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Vector3 screenPos = Input.mousePosition;
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Vector3 hP = hitInfo.point;
                    BoxScript box = hitInfo.transform.gameObject.GetComponent<BoxScript>();
                    Play(hP, box);
                }
            }
        }
    }

    void FixedUpdate()
    {
        /*
        if (Input.GetMouseButtonDown(0) && isEnabled && !end)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 screenPos = Input.mousePosition;
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Vector3 hP = hitInfo.point;
                    BoxScript box = hitInfo.transform.gameObject.GetComponent<BoxScript>();
                    Play(hP, box);
                }
            }
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !end)
        {
            if (isEnabled && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Vector3 screenPos = Input.mousePosition;
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Vector3 hP = hitInfo.point;
                    BoxScript box = hitInfo.transform.gameObject.GetComponent<BoxScript>();
                    Play(hP, box);
                }
            }
        }
        */
    }

    public void Play(Vector3 hitPoint, BoxScript box)
    {
        if (isEnabled)
        {
            isEnabled = false;
            StartCoroutine(PlayCoroutine(hitPoint, box));
        }
    }

    private IEnumerator PlayCoroutine(Vector3 hitPoint, BoxScript box)
    {
        GameObject ob = BoxManagerScript.instance.PlaceBox(hitPoint, box, color, colors[color]);
        BoxScript box1 = ob.GetComponent<BoxScript>();
        List<GameObject> toDestroy = new List<GameObject>();
        toDestroy.Add(ob);
        box1.Check(color, toDestroy);
        yield return new WaitForSeconds(0.1f);
        if (toDestroy.Count > 1)
        {
            score += ScoreScript.instance.CountScore(toDestroy.Count);
            foreach (GameObject gO in toDestroy)
            {
                Vector3 pos = gO.transform.position;
                Color32 col = colors[gO.GetComponent<BoxScript>().Color];
                BoxManagerScript.instance.BoxInGame.Remove(gO);
                Destroy(gO);
                GameObject g = Instantiate(destroyPrefab);
                g.transform.position = pos;
                /*
                var mainMod = g.GetComponent<ParticleSystem>().main;
                mainMod.startColor = new ParticleSystem.MinMaxGradient(col);
                */
                g.GetComponent<Renderer>().material.color = col;
                g.GetComponent<ParticleSystem>().Play();
                Destroy(g, 3);
            }
            GuiScript.instance.SetScore(score);
        }
        if (SetNewColor())
        {
            End();
        }
        else
        {
            yield return new WaitForSeconds(0.4f);
            isEnabled = true;
        }
    }

    private bool SetNewColor()
    {
        List<int> lis = BoxManagerScript.instance.GetColorsInPlay();
        if (lis.Count > 0)
        {
            color = lis[Random.Range(0, (lis.Count - 1))];
            GuiScript.instance.UpdateColor(colors[color]);
        }
        else
        {
            return true;
        }
        return false;
    }

    private void End()
    {
        end = true;
        isEnabled = false;
        GuiScript.instance.ShowControlPanel(false);
        if (ScoreScript.instance.IsNewBest(score, key))
        {
            KeyScript.instance.SaveKeyValue(score, key);
            GuiScript.instance.SetScoreRecord(true, score, score);
        }
        else
        {
            GuiScript.instance.SetScoreRecord(false, score, target);
        }
        if (ScoreScript.instance.IsNewBestTime(time, key + "Time"))
        {
            KeyScript.instance.SaveKeyValue(time, key + "Time");
            GuiScript.instance.SetTimeRecord(true,
                TimeScript.instance.RefactorTime(time),
                TimeScript.instance.RefactorTime(time));
        }
        else
        {
            GuiScript.instance.SetTimeRecord(false,
                 TimeScript.instance.RefactorTime(time),
                  TimeScript.instance.RefactorTime(bestTime));
        }
        GuiScript.instance.ShowEndMenu(true);
    }

    public void ChangeStrategy()
    {
        int index = GuiScript.instance.StrategyDropValue();
        switch (index)
        {
            case 0:
                strategy = new CubeStrategy();
                break;
            case 1:
                strategy = new PyramidStrategy();
                break;
            case 2:
                strategy = new DiamndStrategy();
                break;
            default:
                break;
        }
    }

    public void ChangeSize()
    {
        int index = GuiScript.instance.SizeDropValue();
        switch (index)
        {
            case 0:
                size = 5;
                Camera.main.transform.position = camPosNear;
                break;
            case 1:
                size = 7;
                Camera.main.transform.position = camPosFar;
                break;
            default:
                break;
        }
    }

    public void ChangeColorsCount()
    {
        colorNumber = GuiScript.instance.ColorDropValue() + 3;
    }

    public void ReturnToMenu()
    {
        end = true;
        BoxManagerScript.instance.DestroyAll();
        GuiScript.instance.ShowEndMenu(false);
        GuiScript.instance.ShowControlPanel(false);
        GuiScript.instance.ShowMainMenu(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
