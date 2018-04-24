using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetObject : MonoBehaviour{

    public GameObject Fire;
    public GameObject SubmitButton;
    public static ArrayList SelectedFoodList = new ArrayList();
    public CustomerSpawn customerSpawn;
    private Inventory inventory;
    private Item item;
    private CustomerController CC;
	private AudioManager audioManager;
    public GameObject Water;
    public GameObject wink;
    public bool isAnswering = false;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
		audioManager = FindObjectOfType<AudioManager>();
    }

     
    // Update is called once per frame
    public void Update() {


        if (Fire.activeInHierarchy)                    //disable submit button
        {
            audioManager.StopPlayBG();
            SubmitButton.SetActive(false);
            inventory.RemoveAllItem();
        }
        if (Water.activeInHierarchy)
        {
            audioManager.StopPlayBG();
            SubmitButton.SetActive(false);
            inventory.RemoveAllItem();
        }
        if (wink.activeInHierarchy)
        {
            audioManager.StopPlayBG();
            inventory.RemoveAllItem();
        }

        //   if (SelectedTime >= 2.2f)
        // {
        /* GetTemp.enabled = false;
         string temp = FoodManager.PassObjcetName();
         Debug.Log("The selection is :" + temp);
         if (temp == "Prop_RubbishBin_02")
         {
             audioManager.Play("Remove");
             inventory.RemoveItem();
         }
         else if (temp == "submit")
         {
             CC = FindObjectOfType<CustomerController>();
             CC.CheckFood();
             inventory.RemoveAllItem();
         }
         else if (temp == "fireex" && Fire.activeInHierarchy)
         {
             Fire.SetActive(!Fire.activeInHierarchy);
             SubmitButton.SetActive(!SubmitButton.activeInHierarchy);
         }
         else
         {
             audioManager.Play("Select");
             item = Resources.Load<Item>(temp);
             Debug.Log(item);
             inventory.AddItem(item);
         }

         //}
         */

    }
    public void SelectItem()
    {
            string temp = FoodManager.PassObjcetName();
        Debug.Log("The selection is :" + temp);
            switch (temp)
            {
                case "Prop_RubbishBin_02":
                    audioManager.Play("Remove");
                    inventory.RemoveItem();
                    break;
                case "submit":
                isAnswering = true;
                    CC = FindObjectOfType<CustomerController>();
                    CC.CheckFood();
                    inventory.RemoveAllItem();
                    break;
                case "fireex":
                    if (Fire.activeInHierarchy == true)
                    {
                        audioManager.Play("FireExtinguisher");
                        customerSpawn.unhideCustomer();
                        Fire.SetActive(false);
                        SubmitButton.SetActive(true);
                        audioManager.StartPlayBG();
                    }
                    break;
                case "TurnOffWater":
                    if (Water.activeInHierarchy == true)
                    {
                        customerSpawn.unhideCustomer();
                        Water.SetActive(false);
                        SubmitButton.SetActive(true);
                        audioManager.Play("TurnOffWater");
                        audioManager.StartPlayBG();
                    }
                    break;
                default:
                    audioManager.Play("Select");
                    item = Resources.Load<Item>(temp);
                    Debug.Log(item);
                    inventory.AddItem(item);
                    break;

            }
        
    }
}
