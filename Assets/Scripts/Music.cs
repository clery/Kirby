using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    [SerializeField]
    private AudioClip _onDeathClip;

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
        GetComponent<AudioSource>().clip = _onDeathClip;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().PlayDelayed(1f);
    }
}
