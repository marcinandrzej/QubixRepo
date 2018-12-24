using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScript : MonoBehaviour
{
    public static GuiScript instance;
    public Image currentColor;
    public GameObject menuPanel;
    public GameObject controlPanel;
    public GameObject mainMenuPanel;
    public GameObject scorePanel;
    public Dropdown strategyDropList;
    public Dropdown sizeDropList;
    public Dropdown colorDropList;
    public Text scoreTxt;
    public Text targetTxt;
    public Text timeTxt;
    public Text bestTimeTxt;
    public Text scoreRecordTxt;
    public Text timeRecordTxt;
    public Image timeFillImage;
    public GameObject manuButton;
    // Use this for initialization
    void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateColor(Color32 col)
    {
        currentColor.color = col;
    }

    public void ShowEndMenu(bool show)
    {
        menuPanel.SetActive(show);
    }

    public void ShowMainMenu(bool show)
    {
        mainMenuPanel.SetActive(show);
    }

    public void ShowControlPanel(bool show)
    {
        controlPanel.SetActive(show);
        currentColor.gameObject.SetActive(show);
        scorePanel.SetActive(show);
        timeTxt.gameObject.SetActive(show);
        timeFillImage.gameObject.SetActive(show);
        manuButton.SetActive(show);
    }

    public int StrategyDropValue()
    {
        return strategyDropList.value;
    }

    public int SizeDropValue()
    {
        return sizeDropList.value;
    }

    public int ColorDropValue()
    {
        return colorDropList.value;
    }

    public void SetScore(int score)
    {
        scoreTxt.text = "Score: " + score.ToString();
    }

    public void SetTarget(int score)
    {
        targetTxt.text = "Best: " + score.ToString();
    }

    public void SetTime(string score)
    {
        timeTxt.text = score;
    }

    public void SetBestTime(string score)
    {
        bestTimeTxt.text = "Best: " + score;
    }

    public void SetScoreRecord(bool isRecord, int score, int record)
    {
        if (isRecord)
        {
            scoreRecordTxt.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            scoreRecordTxt.color = new Color32(255, 255, 255, 255);
        }
        scoreRecordTxt.text = "Score: " + score.ToString() + "\n" + "Best score: " + record.ToString();
    }

    public void SetTimeRecord(bool isRecord, string score, string record)
    {
        if (isRecord)
        {
            timeRecordTxt.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            timeRecordTxt.color = new Color32(255, 255, 255, 255);
        }
        timeRecordTxt.text = "Time: " + score + "\n" + "Best time: " + record;
    }

    public void SetTimeImg(int time, int maxTime)
    {
        float fill = Mathf.Abs(((float)(maxTime - time) / (float)maxTime));
        timeFillImage.fillAmount = fill;
    }
}
