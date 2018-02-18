using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour {

    public CustomerSpawn customerSpawn;
    public RecipeManager recipeManager;
    public GameObject hintBtn;
    public GameObject hintFoods;
    public ScoreManager scoreManager;
    ReportNLevelManager RnL;
    DistractionManager distractionManager;

    public Text hintTitleText;
    public float showingTime;
    public int usedHintScore;
    private bool isButtonStart;
    private float tempShowingTime;
    private bool showHint = false;
    string currentHintFood;
    private int hintCounter;
    private int afterDistractionUsedHintCounter;
    
    public const int numItemSlots = 4;
    public Image[] itemImages = new Image[numItemSlots];
    public Text[] itemName = new Text[numItemSlots];

    // Use this for initialization
    void Start()
    {
        distractionManager = FindObjectOfType<DistractionManager>();
        isButtonStart = false;
        hintBtn.SetActive(false);
        tempShowingTime = showingTime;
    }
    // Update is called once per frame
    void Update () {
        if (recipeManager.isStart && !isButtonStart && customerSpawn.currentCustomer != null)
        {
            hintBtn.SetActive(true);
            isButtonStart = true;
        }
        else if ((!recipeManager.isStart && isButtonStart ) || customerSpawn.currentCustomer == null)
        {
            hintBtn.SetActive(false);
            isButtonStart = false;
        }
        if (recipeManager.isStart&&customerSpawn.currentCustomer != null&&showHint)
        {
            int count = 1;
            currentHintFood = customerSpawn.chosenDish;
            Recipe CorrectRecipeFoods = Resources.Load<Recipe>("recipes/" + currentHintFood);
            //hintTitleText.text = "Hint of " + CorrectRecipeFoods.ChineseName;                     //Eng
            hintTitleText.text = "菜單 " + CorrectRecipeFoods.ChineseName + "的提示";               //Chi
            for (int i = 0; i < CorrectRecipeFoods.foods.Length; i++)
            {
                itemName[i].enabled = true;
                itemImages[i].enabled = true;
                itemImages[i].sprite = CorrectRecipeFoods.foods[i].sprite;
                itemName[i].text = (count + ". " + CorrectRecipeFoods.foods[i].ChineseName + "\n");        //Chi
               // itemName[i].text = (count + ". " + CorrectRecipeFoods.foods[i].name + "\n");       //Eng
                count++;
            }
            showingTime -= Time.deltaTime;
            if (showingTime < 0)
            {
                hintBtn.SetActive(true);
                hintFoods.SetActive(false);
                hintTitleText.enabled = false;
                showHint = false;
				emptyRecipe ();
            }
        }
        if (customerSpawn.isCurrentFinished && recipeManager.isStart)
        {
            RnL = FindObjectOfType<ReportNLevelManager>();
            RnL.usedHintsForEachCustomer.Add(hintCounter);
            RnL.numOfHintsUsedAfterDistractionHappend.Add(afterDistractionUsedHintCounter);
            Debug.Log("usedhints" + hintCounter);
            Debug.Log("after dis usedhint" + afterDistractionUsedHintCounter);
            hintCounter = 0;
            afterDistractionUsedHintCounter = 0;
            if (distractionManager)
                distractionManager.isDistractioHappened = false;
            customerSpawn.isCurrentFinished = false;
        }
    }

    public void showHintHandler()
    {
        if (recipeManager.isStart)
        {
            ++hintCounter;
            if (distractionManager)
            {
                if (distractionManager.isDistractioHappened)
                    ++afterDistractionUsedHintCounter;
            }
            showHint = true;
            scoreManager.levelTotalScore -= usedHintScore;
            hintBtn.SetActive(false);
            hintFoods.SetActive(true);
            hintTitleText.enabled = true;
            showingTime = tempShowingTime;
        }
    }

	public void emptyRecipe()
	{
		for (int i = 0; i < numItemSlots; i++) {
			itemName[i].enabled = false;
			itemImages[i].enabled = false;
		}
}
}
