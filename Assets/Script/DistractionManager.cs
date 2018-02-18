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
    void Start () {
        Hiddentimer = 0f;
        StartCoroutine(SetDistraction());
    }

    void Update()
    {
        if (wink.activeInHierarchy)
        {
            Hiddentimer += Time.deltaTime;

            if (Hiddentimer >= 3.5f)
            {
                Debug.Log(wink.transform.GetChild(0).GetComponentInChildren<Text>().text);
                wink.transform.GetChild(0).GetComponentInChildren<Text>().gameObject.SetActive(true);
            }
            if (Hiddentimer >= 5f)
            {
                wink.SetActive(false);
                isDistractioHappened = false;
                customerSpawn.unhideCustomer();
                audioManager.StartPlayBG();

            }
        }
    }

    IEnumerator SetDistraction()
    {
        yield return new WaitUntil(() => recipeManager.isStart);
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));
        int RandomSelect = Random.Range(0, 3);
        Debug.Log(RandomSelect);
        switch (RandomSelect)
        {
            case 0:
                if (recipeManager.isStart && customerSpawn.currentCustomer)// && !water.activeInHierarchy)                //set fire
                {
                    fire.SetActive(true);
                    isDistractioHappened = true;
                    customerSpawn.hideCustomer();
                }
                break;
            case 1:
                if (recipeManager.isStart && customerSpawn.currentCustomer)// && !fire.activeInHierarchy)                //set water
                {
                    water.SetActive(true);
                    isDistractioHappened = true;
                    customerSpawn.hideCustomer();
                }
                break;
            case 2:
                if (recipeManager.isStart && customerSpawn.currentCustomer)// && !fire.activeInHierarchy)                //set water
                {
                    wink.SetActive(true);
                    isDistractioHappened = true;
                    customerSpawn.hideCustomer();
                }
                break;
            default:
                break;
        }
    }
}
