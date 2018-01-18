﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetObject : MonoBehaviour{

    public float SelectedTime =0f;
    public static ArrayList SelectedFoodList = new ArrayList();
    private GetObject GetTemp;
    private Inventory inventory;
    private Item item;
    private CustomerController CC;

    // Use this for initialization
    void Start () {
        GetTemp = GetComponent<GetObject>();
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnDisable()
    {
        SelectedTime = 0f;
    }

    // Update is called once per frame
    public void Update () {

        SelectedTime += Time.deltaTime;

        if (SelectedTime >= 2f)
        {
            GetTemp.enabled = false;
            string temp = FoodManager.PassObjcetName();
            Debug.Log("The selection is :"+ temp);
			if (temp == "Prop_RubbishBin_02")
            {
                inventory.RemoveItem();
            }
            else if (temp == "submit")
            {
                CC = FindObjectOfType<CustomerController>();
                CC.CheckFood();
                inventory.RemoveAllItem();
            }
            else
            {
				item = Resources.Load<Item>(temp);
                    Debug.Log(item);
                    inventory.AddItem(item);
            }
        }
	}


}
