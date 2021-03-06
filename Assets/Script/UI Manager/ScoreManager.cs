﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public CustomerSpawn customerSpawn;
    public RecipeManager recipeManager;
    public ReportNLevelManager RnL;
    public GameObject scoreCanvas;
	public GameObject leftBlocker;
	public GameObject rightBlocker;
    public Text scoreText;
    public Text scoreTitle;

    public int levelTotalScore = 0; //The total socre user got
    public int scoreSplitPoint; //To control the score arrangement
    public int levelPerfectScore; // The full socre of the level

    const int numItemSlots = 5;
    public Image[] StarImage = new Image[numItemSlots];
    // Use this for initialization

    // Update is called once per frame
    void Update () {
		if ((customerSpawn.customerServed == customerSpawn.totalCustomer)&&!scoreCanvas.activeSelf)
        {
            scoreCanvas.SetActive(true);
			leftBlocker.SetActive(true);
			rightBlocker.SetActive(true);
            showStar(showStarCalculator(levelTotalScore));
            //scoreText.text = levelTotalScore.ToString();
            setTitle();
            recipeManager.isStart = false;
            RnL.isLevelEnd = true;
        }


    }
    private int showStarCalculator (int score) //Score arrangement
    {
        if (score >= levelPerfectScore - scoreSplitPoint)
        {
            return 5;
        }
        else if (score < levelPerfectScore - scoreSplitPoint && score >= levelPerfectScore - scoreSplitPoint * 2)
        {
            return 4;
        }
        else if (score < levelPerfectScore - scoreSplitPoint*2 && score >= levelPerfectScore - scoreSplitPoint * 3)
        {
            return 3;
        }
        else if (score < levelPerfectScore - scoreSplitPoint * 3 && score >= levelPerfectScore - scoreSplitPoint * 4)
        {
            return 2;
        }
        else if (score < levelPerfectScore - scoreSplitPoint * 4 && score >= levelPerfectScore - scoreSplitPoint * 5)
        {
            return 1;
        }
        else
            return 3;
    }

    private void showStar (int StarToShow) //Star controller
    {
        for (int i = 0; i<StarToShow; i++)
        {
            if (!StarImage[i].enabled)
                StarImage[i].enabled = true;
        }
    }

    private void setTitle()
    {
        string username = PlayerPrefs.GetString("UserName").ToString();
		int currentLevel = PlayerPrefs.GetInt ("LevelCounter");
            int userScoreSave = PlayerPrefs.GetInt("Level" + currentLevel + "Score");
            if (userScoreSave == 0)
                scoreTitle.text = username + "恭喜曬你完成任務!";
            else if (userScoreSave <= levelTotalScore)
                scoreTitle.text = username + "今次沒有太大進步，下次再努力！";
            else if (userScoreSave > levelTotalScore)
                scoreTitle.text = username + "今次有進步，再接再厲！";
        }
    }
