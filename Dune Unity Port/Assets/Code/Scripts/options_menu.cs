using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class options_menu : MonoBehaviour
{
    // public AudioMixer musicAudioMixer;
    private Dropdown resolutionDropdown;
    public string nameOfTheScreenResolutionDropDown;
    public string nameOfTheMainHolder;
    public string nameOfTheOptionsHolder;
    private GameObject mainHolder;
    private GameObject optionsHolder;
    Resolution[] resolutions;
    public void Start()
    {
        // Getters
        mainHolder = GameObject.Find(nameOfTheMainHolder);
        optionsHolder = GameObject.Find(nameOfTheOptionsHolder);
        nameOfTheScreenResolutionDropDown = "ScreenResolution";
        resolutionDropdown = GameObject.Find(nameOfTheScreenResolutionDropDown).GetComponent<Dropdown>();
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length;++i)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        //musicAudioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool set)
    {
        Screen.fullScreen = set;
    }

    public void SetScreenResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void Back()
    {
        optionsHolder.SetActive(false);
        mainHolder.SetActive(true);
    }
}
