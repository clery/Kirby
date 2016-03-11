using UnityEngine;
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
}
