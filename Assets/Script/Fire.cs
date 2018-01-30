using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public int waitMin;
    public int waitMax;
    public RecipeManager recipeManager;
    public CustomerSpawn customerSpawn;
    public GameObject fire;

    void Start () {
        StartCoroutine(setFire());
    }
	
	void Update () {
        
    }

    IEnumerator setFire()
    {
        yield return new WaitUntil(() => recipeManager.isStart);
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));
        fire.SetActive(true);
        customerSpawn.hideCustomer();
    }
}
