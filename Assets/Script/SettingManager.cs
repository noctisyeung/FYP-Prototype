using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {
    public Dropdown graphicCanvas;
	// Use this for initialization
	void Start () {
        int dropIndex = QualitySettings.GetQualityLevel();
        graphicCanvas.value = dropIndex;
        graphicCanvas.RefreshShownValue();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
