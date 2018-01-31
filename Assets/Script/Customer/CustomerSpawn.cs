﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawn : MonoBehaviour
{

    public List<GameObject> customers;
    public GameObject bubble;
    public RecipeManager recipeManager;
    public GameObject currentCustomer;
    public GameObject fire;
    private Text dishText;
    private AudioManager audioManager;

    [HideInInspector]
    public string chosenDish;
    public int totalCustomer;
    public int minSpawnWait;
    public int maxSpawnWait;
    public int customerServed = 0;

    public bool killMode;

    void Start()
    {
        //audioManager = FindObjectOfType<AudioManager>();
        dishText = bubble.GetComponentInChildren<Text>();
        StartCoroutine(spawnCustomer());
    }

    void Update()
    {
        if (killMode)
        {
            killMode = false;

            if (currentCustomer)
                destroyCustomer();
        }
    }

    IEnumerator spawnCustomer()
    {
        GameObject randCustomer;
        string randRecipe;

        yield return new WaitUntil(() => recipeManager.isStart);
        List<string> recipe = new List<string>(recipeManager.chosenDish);

        while (true)
        {
            yield return new WaitUntil(() => !currentCustomer);
            yield return new WaitUntil(() => !fire.activeInHierarchy);

            if (customerServed >= totalCustomer)
            {
                yield break;
            }

            randRecipe = recipe[Random.Range(0, recipe.Count)];
            Recipe SelectRecipe = Resources.Load<Recipe>("recipes/" + randRecipe);
            dishText.text = "老闆整碗 " + SelectRecipe.ChineseName;                //Chi
            //dishText.text = "I want a "+SelectRecipe.ChineseName;                //Eng
            chosenDish = SelectRecipe.name;
            bubble.SetActive(true);

            if (totalCustomer - customerServed <= recipe.Count)
                recipe.Remove(randRecipe);

            //audioManager.Play("Walk");
            randCustomer = customers[Random.Range(0, customers.Count)];
            currentCustomer = Instantiate(randCustomer, transform.position, transform.rotation);
            currentCustomer.transform.parent = gameObject.transform;
            customers.Remove(randCustomer);

            yield return new WaitForSeconds(Random.Range(minSpawnWait, maxSpawnWait));
        }
    }

    public void destroyCustomer()
    {
        Destroy(currentCustomer);
        bubble.SetActive(false);
        currentCustomer = null;
        customerServed++;
    }

    public void hideCustomer()
    {
        if (currentCustomer)
        {
            currentCustomer.SetActive(false);
            bubble.SetActive(false);
        }
    }

    public void unhideCustomer()
    {
        if (currentCustomer)
        {
            currentCustomer.SetActive(true);
            bubble.SetActive(true);
        }
    }
}