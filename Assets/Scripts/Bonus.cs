using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    [SerializeField]
    private float _timeBeforeFall = 0f;
    [SerializeField]
    private Vector2 _fallingSpeedRange;
    [SerializeField]
    private Vector2 _zigZagRange;
    [SerializeField]
    private float _zigZagSpeed = 1f;
    [SerializeField]
    private int _scoreValue = 10;
    private float _speed = 3f;

    private Rigidbody2D _rigidbody;

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
        StopAllCoroutines();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _speed = Random.Range(_fallingSpeedRange.x, _fallingSpeedRange.y);
        Invoke("DestroySelf", 20);
        StartCoroutine("Fall");
    }

    IEnumerator ZigZag()
    {
        float _timer = 0f;

        while (true)
        {
            _timer += Time.deltaTime * _zigZagSpeed;
            _rigidbody.velocity = new Vector2(Mathf.PingPong(_timer, _zigZagRange.y - _zigZagRange.x) - (_zigZagRange.y - _zigZagRange.x) / 2, _speed);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(_timeBeforeFall);
        _rigidbody.velocity = new Vector2(0, _speed);
        StartCoroutine("ZigZag");
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Score.Instance.Add(_scoreValue);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 2f);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
        }
    }
}


