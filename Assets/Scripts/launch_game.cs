using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class launch_game : MonoBehaviour {
    
    public AudioMixer mix;
    public Slider slidermusic;
    public Slider slidersound;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("sound") && PlayerPrefs.HasKey("music"))
        {
            slidermusic.value = PlayerPrefs.GetFloat("music");
            slidermusic.value = PlayerPrefs.GetFloat("sound");
        }
        else
        {
         PlayerPrefs.SetFloat("sound", 1);
         PlayerPrefs.SetFloat("music", 1);
            slidermusic.value = PlayerPrefs.GetFloat("music");
            slidermusic.value = PlayerPrefs.GetFloat("sound");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void launchgame()
    {
        SceneManager.LoadScene("level1");
    }
    public void launchsettings(GameObject settings)
    {
        settings.SetActive(true);
    }
    public void savesettings(GameObject settings)
    {
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
