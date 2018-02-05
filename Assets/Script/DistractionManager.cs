using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionManager : MonoBehaviour {

    public int waitMin;
    public int waitMax;
    public RecipeManager recipeManager;
    public CustomerSpawn customerSpawn;
    public GameObject fire;
    public GameObject water;

    private AudioManager audioManager;

    void Start () {
        StartCoroutine(SetDistraction());
    }
	
	void Update () {
        
    }

    IEnumerator SetDistraction()
    {
        yield return new WaitUntil(() => recipeManager.isStart);
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));

        int RandomSelect = Random.Range(0, 2);
        Debug.Log(RandomSelect);
        if (RandomSelect == 0)
        {
            if (recipeManager.isStart && customerSpawn.currentCustomer)// && !water.activeInHierarchy)                //set fire
            {
                fire.SetActive(true);
                customerSpawn.hideCustomer();
            }
        }
        else
        {
            if (recipeManager.isStart && customerSpawn.currentCustomer)// && !fire.activeInHierarchy)                //set water
            {
                water.SetActive(true);
                customerSpawn.hideCustomer();
            }
        }


    }
}
