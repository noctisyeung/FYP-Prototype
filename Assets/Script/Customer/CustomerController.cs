using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{

    public Text text;
    public string chosenDish;
    private Inventory inventory;
    Image bubble;
    Color color;
    string[] recipe = { "testrecipe1" };//, "Twotestrecipe2", "testrecipe3" };
    private CustomerSpawn CS;

    void Start()
    {
        text = GameObject.Find("Dish").GetComponent<Text>();
        // Pick a dish randomly
        chosenDish = recipe[Random.Range(0, recipe.Length)];
        text.text = chosenDish;

        //bubble = GameObject.Find("DishBubble").GetComponent<Image>();
		text.enabled = !text.enabled;
    }
		

    public void CheckFood()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.GetItemInList();
        Item[] PlayerSelectedFood = inventory.GetItemInList();
        Recipe CorrectRecipeFoods = Resources.Load<Recipe>("recipes/"+chosenDish);
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
                Debug.Log("Selected Food incorrect");
                return;
            }
            if (counter == 0)
            {

            for (var k = 0; k < PlayerSelectedLength; k++)
            {
                if (PlayerSelectedFood[k] != null)
                {
                        Debug.Log("Selected Food incorrect");
                        return;
                    }
            }
                Debug.Log("Selected Food correct!!!!!");
                CS = FindObjectOfType<CustomerSpawn>();
                CS.destroyCustomer();
            }
        }
    }
}