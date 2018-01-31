using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ReportNLevelManager : MonoBehaviour {
    public static string userName;
    public static int finalScoreForAllLevel = 0;
    public static int levelCounter = 1;
    public bool isLevelEnd = false;
    private string defaultName = "Super Hero";
    private string apikey = "0d8144c5-4df7-4953-b813-f1104fe86dd1";
    private bool isNameEntered = false;
    private string serverAddress = "localhost:8099/test";

    public ScoreManager scoreManager;
    public Text userNameText;

	// Use this for initialization
	void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (isNameEntered)
        {
            if (userNameText.text == "")
                userName = defaultName;
            else
                userName = userNameText.text;
            isNameEntered = false;
            Debug.Log(userName);
            //SceneManager.LoadScene("TestLevel2");   
        }

        if (isLevelEnd)
        {
            finalScoreForAllLevel += scoreManager.levelTotalScore;
            StartCoroutine(sendUserDataToDB(userName, scoreManager.levelTotalScore, finalScoreForAllLevel));
            levelCounter++;
            isLevelEnd = false;
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
        form.AddField("username", username);
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



}
