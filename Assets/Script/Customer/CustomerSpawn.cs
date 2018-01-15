using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawn : MonoBehaviour {

    public GameObject[] customers;
    public GameObject currentCustomer;
	public RecipeManager rm;
	string[] dish;
	public string chosenDish;
	public Text text;

    public int totalCustomer;
    int customerServed = 0;

    int spawnWait;
    public int minSpawnWait;
    public int maxSpawnWait;

    public bool killMode;
    
	void Start ()
    {
        totalCustomer = 2;
        StartCoroutine(spawnCustomer());
	}

    void Update()
    {
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
		yield return new WaitUntil(() => rm.isStart);
		List<string> recipe = new List<string> (rm.chosenDish);
		dish = new string[totalCustomer];


        for (int i = 0; i < totalCustomer; i++)
        {
            yield return new WaitUntil(() => !currentCustomer);
            spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            yield return new WaitForSeconds(spawnWait);

			dish[i] = recipe[Random.Range (0, recipe.Count)];
			text.text = dish[i];
			text.enabled = true;
			chosenDish = dish[i];

			if (totalCustomer <= recipe.Count) {
				recipe.Remove(dish[i]);
			}
			Debug.Log ("choose food: "+ chosenDish);
			currentCustomer = Instantiate(customers[Random.Range (0, 2)], this.transform.position, this.transform.rotation);
            currentCustomer.transform.parent = gameObject.transform;
        }
    }

 public void destroyCustomer()
    {
        Destroy(currentCustomer);
		text.enabled = false;
		text.text = "";
        currentCustomer = null;
        customerServed++;
    }
}
