using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    private Inventory inventory;
    public CustomerSpawn CS;
    public ScoreManager scoreManager;
    public int dishScore;
    public int wrongScore;
    public void CheckFood()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.GetItemInList();
        Item[] PlayerSelectedFood = inventory.GetItemInList();
		CS = FindObjectOfType<CustomerSpawn> ();
        scoreManager = FindObjectOfType<ScoreManager>();
		Recipe CorrectRecipeFoods = Resources.Load<Recipe>("recipes/"+CS.chosenDish);
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
                Debug.Log("Selected Food incorrect"+ scoreManager.levelTotalScore);
                dishScore -= wrongScore;
                return;
            }
            if (counter == 0)
            {

            for (var k = 0; k < PlayerSelectedLength; k++)
            {
                if (PlayerSelectedFood[k] != null)
                {
                        Debug.Log("Selected Food incorrect" + scoreManager.levelTotalScore);
                        dishScore -= wrongScore;
                        return;
                    }
            }
                Debug.Log("Selected Food correct!!!!!");
                scoreManager.levelTotalScore += dishScore;
                CS = FindObjectOfType<CustomerSpawn>();
                CS.destroyCustomer();
            }
        }
    }
}