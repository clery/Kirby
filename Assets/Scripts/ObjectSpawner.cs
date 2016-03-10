using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField]
    private Vector2 _timeBetweenSpawnsStart;
    [SerializeField]
    private Vector2 _timeBetweenSpawnsEnd;

    [SerializeField]
    private float _timeBeforeMaxDifficulty = 90f;
    private float _timer;
    [SerializeField]
    private GameObject _object;

	// Use this for initialization
	void Start () {
        _timer = Random.Range(_timeBetweenSpawnsStart.x, _timeBetweenSpawnsStart.y);
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
        this.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        _timer -= Time.deltaTime;
        if (_timer < 0f)
        {
            SpawnObject();
            var min = Mathf.Lerp(_timeBetweenSpawnsStart.x, _timeBetweenSpawnsEnd.x, _timer / _timeBeforeMaxDifficulty);
            var max = Mathf.Lerp(_timeBetweenSpawnsStart.y, _timeBetweenSpawnsEnd.y, _timer / _timeBeforeMaxDifficulty);
            _timer = Random.Range(min, max);
        }
    }

    void SpawnObject()
    {
        GameObject obj = Instantiate<GameObject>(_object);
        obj.transform.SetParent(transform.GetChild(Random.Range(0, transform.childCount - 1)));
        obj.transform.localPosition = Vector3.zero;
        Debug.Log(obj.transform.localScale);
    }
}
