using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour {
    public static string ObjectName;
    // Use this for initialization

    void Start () {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetTheFood()
    {

        ObjectName = this.gameObject.name;
       // Debug.Log("Item Name from FoodManager :"+ObjectName);
    }

    public static string PassObjcetName()
    {
        return ObjectName;
    }


    public void LeaveTheFood()
    {

        ObjectName = null;
    }
}
