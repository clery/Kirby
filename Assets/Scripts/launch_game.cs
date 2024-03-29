﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class launch_game : MonoBehaviour {
    
    public AudioMixer mix;
    public Slider slidermusic;
    public Slider slidersound;
    public GameObject settingsMenu;
    public GameObject replayMenu;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("sound") && PlayerPrefs.HasKey("music"))
        {
            slidermusic.value = PlayerPrefs.GetFloat("music");
            slidersound.value = PlayerPrefs.GetFloat("sound");
        }
        else
        {
            PlayerPrefs.SetFloat("sound", 1);
            PlayerPrefs.SetFloat("music", 1);
            slidermusic.value = PlayerPrefs.GetFloat("music");
            slidersound.value = PlayerPrefs.GetFloat("sound");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu != null && settingsMenu.gameObject.activeSelf == true)
                savesettings(settingsMenu);
            else if (settingsMenu != null && replayMenu.gameObject.activeSelf == false)
                launchMenu(settingsMenu);
            else
                Application.Quit();
        }
	}

    void OnEnable()
    {
        Controller.OnDeath += Handle_OnDeath;
    }

    void OnDisable()
    {
        Controller.OnDeath -= Handle_OnDeath;
    }

    private void Handle_OnDeath(Controller character)
    {
        Invoke("launchReplay", 3f);
    }

    public void launchscene(string scene)
    {
        Time.timeScale = 1f;
        Score.Instance.Reset();
        SceneManager.LoadScene(scene);
    }

    public void quitgame()
    {
        Application.Quit();
    }
    void launchReplay()
    {
        Score.Instance.PublishScore();
        launchMenu(replayMenu);
    }
    public void launchMenu(GameObject menu)
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }
    public void savesettings(GameObject settings)
    {
        Time.timeScale = 1f;
        settings.SetActive(false);
    }
    public void ChangeSounds(Slider slider)
    {
        PlayerPrefs.SetFloat("sound", slider.value);
        mix.SetFloat("sound", Mathf.Lerp(-80, 0, slider.value));
    }

    public void ChangeMusic(Slider slider)
    {
        PlayerPrefs.SetFloat("music", slider.value);
        mix.SetFloat("music", Mathf.Lerp(-80, 0, slider.value));
    }
}
