using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ReportNLevelManager : MonoBehaviour {
    private string userName;
    public static int finalScoreForAllLevel = 0; //Not using right now
    public static int levelCounter = 1;
    public bool isLevelEnd = false;
    private string defaultName = "Super Hero";
    private string apikey = "0d8144c5-4df7-4953-b813-f1104fe86dd1";
    private bool isNameEntered = false;
    private string serverAddress = "localhost:8099/test";
    public List<float> usedTimeForEachCustomer = new List<float>(); //This List will used by customercontroller class
    public List<int> usedHintsForEachCustomer = new List<int>();// This List will used by hint manager class
    public List<int> numOfHintsUsedAfterDistractionHappend = new List<int>();

    public RecipeManager recipeManager;
    public ScoreManager scoreManager;
    public InputField userNameText;

    SettingManager settingManager;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("NumberOfVisit") && SceneManager.GetActiveScene().name == "InputTest")
        {
            settingManager = FindObjectOfType<SettingManager>();
            int numberOfVisit = PlayerPrefs.GetInt("NumberOfVisit");
            ++numberOfVisit;
            settingManager.CloseTheInputScreen();
            PlayerPrefs.SetInt("NumberOfVisit", numberOfVisit);
        }
        else if (!PlayerPrefs.HasKey("NumberOfVisit") && SceneManager.GetActiveScene().name == "InputTest")
        {
            settingManager = FindObjectOfType<SettingManager>();
            settingManager.OpenTheInputScreen();
            PlayerPrefs.SetInt("NumberOfVisit", 1);
        }
        if (SceneManager.GetActiveScene().name == "InputTest" && PlayerPrefs.GetString("UserName")!=null)
        {
            userNameText.text = PlayerPrefs.GetString("UserName").ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (isNameEntered)
        {
            if (userNameText.text == "")
            {
                userName = defaultName;
                userNameText.text = userName;
            }
            else
            {
                userName = userNameText.text;
            }
            isNameEntered = false;
            PlayerPrefs.SetString("UserName", userName);
            Debug.Log(userName);
            //SceneManager.LoadScene("TestLevel2");   
        }

        if (isLevelEnd)
        {
            finalScoreForAllLevel += scoreManager.levelTotalScore;
			PlayerPrefs.SetInt ("LevelCounter", levelCounter);
			PlayerPrefs.SetInt ("Level" + levelCounter + "Score", scoreManager.levelTotalScore);
            /*foreach (float value in usedTimeForEachCustomer){ //Debug use
                Debug.Log("This is the hints lists"+value);
            }*/
            DoSaveData();
            //StartCoroutine(sendUserDataToDB(userName, scoreManager.levelTotalScore, finalScoreForAllLevel));
            levelCounter++;
            isLevelEnd = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(PlayerPrefs.GetString("Level" + levelCounter + "Data"));
            LoadSaveData();
        }  
        
    }

    public void LoadMainScreen()
    {
        isNameEntered = true;
    }

    IEnumerator sendUserDataToDB (string username, int levelScore, int allLevelScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("api", apikey);
        form.AddField("username", PlayerPrefs.GetString("UserName").ToString());
        form.AddField("levelscore", levelScore);
        form.AddField("currentlevel", levelCounter);
        if (levelCounter == 5)
        {
            form.AddField("alllevelscore", allLevelScore);
        }
        UnityWebRequest www = UnityWebRequest.Post(serverAddress, form);
        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
       
        else
        {
            Debug.Log("Form upload complete!");
        }
    }




    public void DoSaveData()
    {
        SavePlayerData levelsave = new SavePlayerData();
        levelsave.theTimeUsedToRememberTheitem = recipeManager.usedTimeForRemember;
        levelsave.theTimeUsedToServeCustomer = usedTimeForEachCustomer;
        levelsave.numOfHintsUsedForEachCustomer = usedHintsForEachCustomer;
        levelsave.numOfHintsUsedAfterDistractionHappend = numOfHintsUsedAfterDistractionHappend;
        PlayerPrefs.SetString("Level" + levelCounter + "Data", JsonUtility.ToJson(levelsave));
    }

    public void LoadSaveData()
    {
        SavePlayerData levelsave = new SavePlayerData();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Level" + levelCounter + "Data"), levelsave);
        recipeManager.usedTimeForRemember = levelsave.theTimeUsedToRememberTheitem;
        usedTimeForEachCustomer = levelsave.theTimeUsedToServeCustomer;
        usedHintsForEachCustomer = levelsave.numOfHintsUsedForEachCustomer;
        numOfHintsUsedAfterDistractionHappend = levelsave.numOfHintsUsedAfterDistractionHappend;
    }
}

class SavePlayerData
{
    public int theTimeUsedToRememberTheitem;
    public List<float> theTimeUsedToServeCustomer = new List<float>();
    public List<int> numOfHintsUsedForEachCustomer = new List<int>();
    public List<int> numOfHintsUsedAfterDistractionHappend = new List<int>();
}
