using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DistractionManager : MonoBehaviour {

    public int waitMin;
    public int waitMax;
    public bool isDistractioHappened = false; //used for the hint manager to control the hints count after distraction happened
    public RecipeManager recipeManager;
    public CustomerSpawn customerSpawn;
    public GameObject fire;
    public GameObject water;
    public GameObject wink;
    public float Hiddentimer;
    public AudioManager audioManager;
	public Image AnsImage;	
	public Sprite tick;
    GetObject getObject;
    Coroutine distractCor = null;
    public bool RestartDistraction = false;

    void Start () {
        Hiddentimer = 0f;
        distractCor = StartCoroutine(SetDistraction());
        getObject = FindObjectOfType<GetObject>();
    }

    void Update()
    {
        if (wink.activeInHierarchy)                     //for donothing distraction
        {
            Hiddentimer += Time.deltaTime;

            if (Hiddentimer >= 3.5f)
            {
         //       Debug.Log(wink.transform.GetChild(0).GetComponentInChildren<Text>().text);
                wink.transform.GetChild(0).GetComponentInChildren<Text>().gameObject.SetActive(true);
            }
            if (Hiddentimer >= 5f)
            {
                wink.SetActive(false);
                //isDistractioHappened = false;
                customerSpawn.unhideCustomer();
                audioManager.StartPlayBG();

            }
        }
        if (RestartDistraction == true)
        {
            waitMin = 1;
            waitMax = 1;
            StartCoroutine(SetDistraction());
            Debug.Log("distraction restarted");
            RestartDistraction = false;
        }
		if (customerSpawn.isAnswering)
        {
            StopCoroutine(distractCor);
			customerSpawn.isAnswering = false;
        }
    }
    private void DistractionStart()
    {
        isDistractioHappened = true;
        customerSpawn.hideCustomer();
    }



    IEnumerator SetDistraction()
    {
        Sprite ansImageName = AnsImage.sprite;
        //Debug.Log (ansImageName);
        int waitingTime = Random.Range(waitMin, waitMax);
        //		Debug.Log ("start dis");
        yield return new WaitUntil(() => recipeManager.isStart);                //check the game is start
                                                                                //		Debug.Log("Game start");
        yield return new WaitForSeconds(waitingTime);                           //wait for random time
        Debug.Log("Wait for"+ waitingTime);
        yield return new WaitUntil(() => customerSpawn.currentCustomer);       //check if there are any customer
        Debug.Log("passed");
        yield return new WaitUntil(() => ansImageName != tick);                //make sure the distraction will not happen in showing the correct tick                                                                             //		
        Debug.Log("passed tick it is :"+ansImageName);
        //Debug.Log (customerSpawn.currentCustomer);
        yield return new WaitForSeconds(3);										//buff time for next customer if the distraction is happen in the waiting custion spawn time
            int RandomSelect = Random.Range(0, 3);
            //Debug.Log(RandomSelect);

            switch (RandomSelect)
            {
                case 0:
                    fire.SetActive(true);
                    DistractionStart();
                    break;
                case 1:
                    water.SetActive(true);
                    DistractionStart();
                    break;
                case 2:
                    wink.SetActive(true);
                    DistractionStart();
                    break;
                default:
                    break;
            }



        
    }
}
