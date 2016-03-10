using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour {

    [SerializeField]
    private Text _text;
    [SerializeField]
    private Animator _animator;

    private string _prefix = "";

	// Use this for initialization
	void Start () {
        _prefix = _text.text;
        _text.text = _prefix + 0;
	}

    void OnEnable()
    {
        Score.OnAddScore += Handle_OnAddScore;
    }

    private void Handle_OnAddScore()
    {
        _animator.SetTrigger("AddScore");
    }

    // Update is called once per frame
    void Update () {
        _text.text = _prefix + Score.Instance.Value;
	}


}
