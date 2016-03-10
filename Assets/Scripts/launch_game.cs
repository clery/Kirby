using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class launch_game : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void launchgame()
    {
        SceneManager.LoadScene("level1");
    }
}
