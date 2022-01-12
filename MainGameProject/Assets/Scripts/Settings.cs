using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour{

    public AudioMixer audioMixer;
    public AudioMixer sfxaudioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake(){
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    void Start(){
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        sfxaudioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setVolume(float volume){
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void setSFXVolume(float volume){
        sfxaudioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex + 2);
    }

    public void setFullScreen(bool setScreen){
        Screen.fullScreen = setScreen;
    }

    public void setResolution(int resIndex){
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
