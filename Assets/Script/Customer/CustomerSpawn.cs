using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawn : MonoBehaviour {

    public GameObject[] customers;
    public GameObject currentCustomer;

    public int totalCustomer;
    int customerServed = 0;

    int spawnWait;
    int minSpawnWait = 3;
    int maxSpawnWait = 5;

    // Testing variable
    public bool killMode;
    
	void Start ()
    {
        totalCustomer = 3;
        StartCoroutine(spawnCustomer());
	}

    void Update()
    {
        // Testing remove customer
        if (killMode)
        {
            killMode = false;

            if (currentCustomer)
            {
                destroyCustomer();
            }
        }

        // End game
        if (customerServed == totalCustomer)
        {
            /*Text gameOver = GameObject.Find("GameOver").GetComponent<Text>();
            Color color = gameOver.color;
            color.a = 1;
            gameOver.color = color;*/
        }
    }

    IEnumerator spawnCustomer()
    {
        for (int i = 0; i < totalCustomer; i++)
        {
            yield return new WaitUntil(() => !currentCustomer);
            spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            yield return new WaitForSeconds(spawnWait);

            currentCustomer = Instantiate(customers[Random.Range(0, 2)], new Vector3(-6.2f, 8, 25), gameObject.transform.rotation);
            currentCustomer.transform.parent = gameObject.transform;
        }
    }

 public void destroyCustomer()
    {
        Destroy(currentCustomer);
		Text text = GameObject.Find("Dish").GetComponent<Text>();
		text.enabled = !text.enabled;
        currentCustomer = null;
        customerServed++;
    }
}
