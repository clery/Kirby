using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetBS : MonoBehaviour {
    public GameObject BestScores;

	// Use this for initialization
	void Start () {
        Text txt = BestScores.transform.GetChild(1).GetComponent<Text>();
        if (PlayerPrefs.HasKey("BS1"))
            txt.text = "1 : " + PlayerPrefs.GetString("BS1");
        Text txt1 = BestScores.transform.GetChild(2).GetComponent<Text>();
        if (PlayerPrefs.HasKey("BS2"))
            txt1.text = "2 : " + PlayerPrefs.GetString("BS2");
        Text txt2 = BestScores.transform.GetChild(3).GetComponent<Text>();
        if (PlayerPrefs.HasKey("BS3"))
            txt2.text = "3 : " + PlayerPrefs.GetString("BS3");
        Text txt3 = BestScores.transform.GetChild(4).GetComponent<Text>();
        if (PlayerPrefs.HasKey("BS4"))
            txt3.text = "4 : " + PlayerPrefs.GetString("BS4");
        Text txt4 = BestScores.transform.GetChild(5).GetComponent<Text>();
        if (PlayerPrefs.HasKey("BS5"))
            txt4.text = "5 : " + PlayerPrefs.GetString("BS5");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void quitBS()
    {
        BestScores.SetActive(false);
    }
}
