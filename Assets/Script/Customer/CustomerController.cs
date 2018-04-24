using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
	public Sprite tick;
	public Sprite cross;
    public float stopwatch = 0;
    private bool thisCustomerIsEnded = false; //Used in this class only
	private Image answerImage;
    private Inventory inventory;
    public CustomerSpawn CS;
    public ScoreManager scoreManager;
    ReportNLevelManager RnL;
    DistractionManager distractionManager;
    public int dishScore;
    public int wrongScore;
	private AudioManager audioManager;
	private int destroyWait = 5;
	private int wrongCounter = 0;
    private int wrongCounterAfterDistraction = 0;
	public bool isCustomerWrong = false;


	private void Start()
	{
        RnL = FindObjectOfType<ReportNLevelManager>();
        distractionManager = FindObjectOfType<DistractionManager>();
		audioManager = FindObjectOfType<AudioManager>();
		answerImage = GameObject.Find("AnswerImage").GetComponent<Image>();
	}

    private void Update()
    {
        if (!thisCustomerIsEnded)
            stopwatch += Time.deltaTime;
        else
        {
            RnL.usedTimeForEachCustomer.Add(stopwatch);
			RnL.numOfWrongCounterForEachCustomer.Add (wrongCounter);
            RnL.numOfWrongCounterAfterDistractionHappend.Add(wrongCounterAfterDistraction);
			wrongCounter = 0;
            wrongCounterAfterDistraction = 0;
            thisCustomerIsEnded = false;
        }

		//Debug.Log (isCustomerWrong);
    } 

    public void CheckFood()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.GetItemInList();
        Item[] PlayerSelectedFood = inventory.GetItemInList();
		CS = FindObjectOfType<CustomerSpawn> ();
        scoreManager = FindObjectOfType<ScoreManager>();
		Recipe CorrectRecipeFoods = Resources.Load<Recipe>("recipes/"+CS.chosenDish);
        int counter = CorrectRecipeFoods.foods.Length;
        int PlayerSelectedLength = PlayerSelectedFood.Length;
        for (var i = 0; i < CorrectRecipeFoods.foods.Length; i++)
        {
            bool correct = false;
            for (var k = 0; k < PlayerSelectedLength; k++)
            {
                if (CorrectRecipeFoods.foods[i] == PlayerSelectedFood[k])
                {
                    PlayerSelectedFood[k] = null;
                    counter--;
                    Debug.Log("counter:"+counter);
                    correct = true;
                    break;
                }
            }

           if (correct == false)
            {
                Debug.Log("Selected Food incorrect"+ scoreManager.levelTotalScore);
                dishScore -= wrongScore;
				++wrongCounter;
                if (distractionManager.isDistractioHappened)
                    ++wrongCounterAfterDistraction;
				isCustomerWrong = true;
				setWrong();
                return;
            }
            if (counter == 0)
            {

            for (var k = 0; k < PlayerSelectedLength; k++)
            {
                if (PlayerSelectedFood[k] != null)
                {
                        Debug.Log("Selected Food incorrect" + scoreManager.levelTotalScore);
                        dishScore -= wrongScore;
						++wrongCounter;
                        if (distractionManager.isDistractioHappened)
                            ++wrongCounterAfterDistraction;
						isCustomerWrong = true;
						setWrong();
                        return;
                }
            }
                Debug.Log("Selected Food correct!!!!!");
                Debug.Log(stopwatch);
                scoreManager.levelTotalScore += dishScore;
				setCorrect();
                thisCustomerIsEnded = true;
                CS = FindObjectOfType<CustomerSpawn>();
				CS.isCurrentFinished = true;
				Debug.Log ("cc" + CS.isCurrentFinished);
				CS.isAnswering = true;
				CS.Invoke("destroyCustomer", destroyWait);
            }
        }
    }

	private void setCorrect()
	{
		
		var tempColor = answerImage.color;
		tempColor.a = 1f;
		answerImage.color = tempColor;
		answerImage.sprite = tick;
	//	answerImage.enabled = true;
		audioManager.Play("Correct");
		Invoke("destroySet", destroyWait);
	}

	private void setWrong()
	{
		
        var tempColor = answerImage.color;
        tempColor.a = 1f;
        answerImage.color = tempColor;
        answerImage.sprite = cross;
	//	answerImage.enabled = true;
		audioManager.Play("Wrong");
		Invoke("destroySet", destroyWait);
	}

	private void destroySet()
	{
		//answerImage.sprite = null;
		var tempColor = answerImage.color;
		tempColor.a = 0f;
		answerImage.color = tempColor;
	//	answerImage.enabled = false;
	}
}