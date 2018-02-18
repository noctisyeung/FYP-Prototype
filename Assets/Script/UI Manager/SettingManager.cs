using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour {
    public Dropdown graphicCanvas;
    public Slider volumeSlider;
    public Toggle muteToggle;
    public GameObject comfirmMenu;
    public GameObject subMenu;
    public GameObject remindScreen;
    public Canvas mainInputMenu;
    public Text submitButtonText;

    private float oldVolumeSliderValue;


    // Use this for initialization
    void Start () {
        int dropIndex = QualitySettings.GetQualityLevel();
        graphicCanvas.value = dropIndex;
        graphicCanvas.RefreshShownValue();
        if (PlayerPrefs.HasKey("NumberOfVisit"))
        {
            if (PlayerPrefs.GetInt("NumberOfVisit") > 1)
                submitButtonText.text = "修改";
            else
                submitButtonText.text = "提交";
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
            volumeSlider.value = AudioListener.volume = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if (volumeSlider.value != 0)
            muteToggle.isOn = true;
        else
            muteToggle.isOn = false;
        if (Input.GetKeyDown(KeyCode.Escape)) //For android
        {
            if (!comfirmMenu.activeInHierarchy)
            {
                subMenu.SetActive(false);
            }
            else
            {
                comfirmMenu.SetActive(false);
            }
        }
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetMusicVolume(float volume)
    {
            AudioListener.volume = volume;
    }

    public void returnSubMenu()
    {
        comfirmMenu.SetActive(false);
    }

    public void comfirmToDestorySaveData()
    {
        PlayerPrefs.DeleteAll();
        QualitySettings.SetQualityLevel(1);
    }

    public void isMusicOn (bool isMusicOn)
    {
        if (isMusicOn)
        {         
            volumeSlider.value = oldVolumeSliderValue;
        }
        else
        {
            oldVolumeSliderValue = volumeSlider.value;
            volumeSlider.value = 0;
        }
    }

    public void showComfirmMenu()
    {
        comfirmMenu.SetActive(true);
    }

    public void showSubMenu()
    {
        subMenu.SetActive(true);
    }

    public void SaveSetting()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        subMenu.SetActive(false);
    }

    public void CloseTheInputScreen ()
    {
        mainInputMenu.enabled = false;
        remindScreen.SetActive(true);
        submitButtonText.text = "修改";
    }

    public void OpenTheInputScreen ()
    {
        mainInputMenu.enabled = true;
        remindScreen.SetActive(false);
    }
}
