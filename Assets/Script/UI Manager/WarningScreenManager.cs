using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScreenManager : MonoBehaviour {
    public LO_LoadScene loadScene;
	// Use this for initialization
	void Start () {
        StartCoroutine(WarningCounter());
	}
	
	// Update is called once per frame
    IEnumerator WarningCounter()
    {
        yield return new WaitForSeconds(3);
        loadScene.ChangeToScene("InputTest");
    }
}
