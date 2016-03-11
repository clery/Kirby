using UnityEngine;
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
        if (settingsMenu != null && Input.GetKeyDown(KeyCode.Escape) && replayMenu.gameObject.activeSelf == false)
        {
            launchMenu(settingsMenu);
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

    public void launchgame()
    {
        Time.timeScale = 1f;
        Score.Instance.Reset();
        SceneManager.LoadScene("level1");
    }

    public void quitgame()
    {
        Application.Quit();
    }
    void launchReplay()
    {
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
        mix.SetFloat("sound", Mathf.Lerp(-80, 20, slider.value));
    }

    public void ChangeMusic(Slider slider)
    {
        PlayerPrefs.SetFloat("music", slider.value);
        mix.SetFloat("music", Mathf.Lerp(-80, 20, slider.value));
    }
}
