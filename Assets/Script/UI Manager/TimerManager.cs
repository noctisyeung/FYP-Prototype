using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {
    public float startTime;
    private int usedTime;
    Text timeText;
    public bool IsTimeEnd;
	public RecipeManager recipeManager;
	public GameObject score;
    // Use this for initialization
    void Awake()
    {
        timeText = GetComponent<Text>();
        IsTimeEnd = false;
    }
    // Update is called once per frame
    void Update () {
        if (!IsTimeEnd)
        {
            startTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(startTime / 60F);
			int seconds = Mathf.FloorToInt(startTime - minutes * 60);
			timeText.text = "時間: " + minutes + " : " + seconds;
			if (seconds == 0&&minutes == 0 )
            {
                    IsTimeEnd = true;
                
            }
        }
        else
        {
            timeText.text = "夠鐘啦!!";
			if (!recipeManager.isStart&&!score.activeSelf)
			recipeManager.StartTheGame ();
        }
    }
}
