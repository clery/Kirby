using UnityEngine;
using System.Collections;

public class PlatformCrush : MonoBehaviour {

    [SerializeField]
    private Vector2 _depopFrequencyStart;
    [SerializeField]
    private Vector2 _depopFrequencyEnd;
    private float _depopTimer;
    [SerializeField]
    private Vector2 _depopLengthStart;
    [SerializeField]
    private Vector2 _depopLengthEnd;

    [SerializeField]
    private float _timeForMaxDifficulty = 90f;

    [SerializeField]
    private float _fadeTime = 2f;
    private float _timeSinceStart = 0f;

    private int _activeChild = 0;

    private bool _isVisible = true;

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Ground"));
        _depopTimer = Random.Range(_isVisible ? _depopFrequencyStart.x : _depopLengthStart.x, _isVisible ? _depopFrequencyStart.y : _depopLengthStart.y);
    }

    // Update is called once per frame
    void Update () {
        _depopTimer -= Time.deltaTime;
        _timeSinceStart += Time.deltaTime;
        if (_depopTimer < 0f)
        {
            _isVisible = !_isVisible;
            var min = Mathf.Lerp((_isVisible ? _depopFrequencyStart.x : _depopLengthStart.x), (_isVisible ? _depopFrequencyEnd.x : _depopLengthEnd.x), _timeSinceStart / _timeForMaxDifficulty);
            var max = Mathf.Lerp((_isVisible ? _depopFrequencyStart.y : _depopLengthStart.y), (_isVisible ? _depopFrequencyEnd.y : _depopLengthEnd.y), _timeSinceStart / _timeForMaxDifficulty);
//            var min = (_isVisible ? Mathf.Lerp(_depopFrequencyStart.x, _depopFrequencyEnd.x, _timeSinceStart / _timeForMaxDifficulty) : Mathf.Lerp(_depopLengthStart.x, _depopLengthEnd.x, _timeSinceStart / _timeForMaxDifficulty));
//            var max = (_isVisible ? Mathf.Lerp(_depopFrequencyStart.y, _depopFrequencyEnd.y, _timeSinceStart / _timeForMaxDifficulty) : Mathf.Lerp(_depopLengthStart.y, _depopLengthEnd.y, _timeSinceStart / _timeForMaxDifficulty));
            _depopTimer = Random.Range(min, max);
            StopCoroutine("FadeOut");
            StopCoroutine("FadeIn");
            if (!_isVisible)
            {
                transform.GetChild(_activeChild).gameObject.SetActive(false);
                _activeChild = Random.Range(0, transform.childCount - 1);
            }
            if (_isVisible)
            {
                _depopTimer += _fadeTime;
                StartCoroutine("FadeIn", _fadeTime);
            }

        }
        else if (_depopTimer < _fadeTime && _isVisible)
        {
            StartCoroutine("FadeOut", _depopTimer);
        }
	}

    IEnumerator FadeOut(float depopTime)
    {
        var max = depopTime;
        var child = transform.GetChild(_activeChild);
        var color = Color.white;
        while (depopTime > 0f)
        {
            depopTime -= Time.deltaTime;
            color.a = depopTime / max;
            for (int i = 0; i < child.childCount; ++i)
                child.GetChild(i).GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }

    IEnumerator FadeIn(float depopTime)
    {
        var time = 0f;
        var child = transform.GetChild(_activeChild);
        var color = Color.white;
        child.GetComponent<BoxCollider2D>().enabled = false;
        child.gameObject.SetActive(true);
        while (time < depopTime)
        {
            time += Time.deltaTime;
            color.a = time / depopTime;
            for (int i = 0; i < child.childCount; ++i)
                child.GetChild(i).GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
        for (int i = 0; i < transform.GetChild(_activeChild).childCount; ++i)
            transform.GetChild(_activeChild).GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
        child.GetComponent<BoxCollider2D>().enabled = true;
    }
}
