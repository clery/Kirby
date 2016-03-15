using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : Singleton<Score> {

    private float _score = 0;
    public int Value
    {
        get { return ((int)_score); }
    }
    private float _scoreMultiplier = 5;

    public delegate void ScoreAction();
    public static event ScoreAction OnAddScore;

    public void Update()
    {
        _score += Time.deltaTime * _scoreMultiplier;
    }

    public void Add(int value)
    {
        Add((float)value);
    }

    public void Add(float value)
    {
        _score += value;
        if (OnAddScore != null)
            OnAddScore();
    }

    public void Reset()
    {
        _score = 0;
    }

    public void PublishScore()
    {
        string[] texts = new string[5];
        float score = _score;

        for (int i = 0; i < 5; ++i)
        {
            texts[i] = (PlayerPrefs.HasKey("BS" + (i + 1)) ? PlayerPrefs.GetString("BS" + (i + 1)) : "0");
            if (score > float.Parse(texts[i]))
            {
                PlayerPrefs.SetString("BS" + (i + 1), ((int)score).ToString());
                score = float.Parse(texts[i]);
            }
        }
    }
}
