using UnityEngine;
using System.Collections;

public class Stalactite : MonoBehaviour {

    [SerializeField]
    private float _timeBeforeFall = 3f;
    [SerializeField]
    private Vector2 _fallingSpeedRange;
    private float _speed = 5f;

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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.enabled = false;
    }

    // Use this for initialization
    void Start () {
        _speed = Random.Range(_fallingSpeedRange.x, _fallingSpeedRange.y);
	}
	
	// Update is called once per frame
	void Update () {
        _timeBeforeFall -= Time.deltaTime;
        if (_timeBeforeFall <= 0f)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed);
        if (transform.position.y <= -2)
            Destroy(gameObject);
    }


}
