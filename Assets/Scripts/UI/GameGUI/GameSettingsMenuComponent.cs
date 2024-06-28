using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class GameSettingsMenu : GameMenuComponent
{
    [Header("Sliders")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider masterVolumeSlider;

    [Header("Dropdown")]
    public TMP_Dropdown languageDropdown;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer; 

    float defaultVolume = 0.75f;

    private void Start() => InitializeControls();

    private void InitializeControls()
    {
        InitializeSoundValues();
        InitializeLanguageOptions();
        ActivateListeners();
    }

    void OnEnable() => ActivateListeners();
    void OnDisable() => DeactivateListeners();
    void SaveSettings() => PlayerPrefs.Save();

    public void OnBackButtonClicked()
    {
        SaveSettings(); 
        controller.OpenPauseMenu();  
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);  
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);  
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void ChangeLanguage(int index)
    {
        PlayerPrefs.SetInt("language", index);
        Debug.Log("Language Changed: " + languageDropdown.options[index].text);
    }

    void ActivateListeners()
    {
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    void DeactivateListeners()
    {
        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        languageDropdown.onValueChanged.RemoveAllListeners();
    }

    void InitializeSoundValues()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", defaultVolume);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume", defaultVolume);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", defaultVolume);

        if (!PlayerPrefs.HasKey("musicVolume")) PlayerPrefs.SetFloat("musicVolume", defaultVolume);
        if (!PlayerPrefs.HasKey("sfxVolume")) PlayerPrefs.SetFloat("sfxVolume", defaultVolume);
        if (!PlayerPrefs.HasKey("masterVolume")) PlayerPrefs.SetFloat("masterVolume", defaultVolume);
    }

    void InitializeLanguageOptions()
    {
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(new List<string> { "English", "Türkçe" });
        languageDropdown.value = PlayerPrefs.GetInt("language", 0);
    }
}
