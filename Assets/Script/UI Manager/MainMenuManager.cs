using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenuManager : MonoBehaviour {
    public string fristLevel;
	public string tutor;
	// Use this for initialization
	void Start () {
        StartCoroutine(SwitchToVR());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTheGame ()
    {
        SceneManager.LoadScene(fristLevel);
    }
		
	public void StartTutor ()
	{
		SceneManager.LoadScene(tutor);
	}

    IEnumerator SwitchToVR()
    {
        // Device names are lowercase, as returned by `XRSettings.supportedDevices`.
        string desiredDevice = "cardboard"; // Or "cardboard".

        XRSettings.LoadDeviceByName(desiredDevice);

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Now it's ok to enable VR mode.
        XRSettings.enabled = true;
    }
}
