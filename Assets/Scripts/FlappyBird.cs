using UnityEngine;
using System.Collections;

public class FlappyBird : MonoBehaviour
{

    private enum EDirection
    {
        LEFT,
        RIGHT
    }

    [SerializeField]
    private float _timeBeforeFall = 0f;
    [SerializeField]
    private Vector2 _fallingSpeedRange;
    [SerializeField]
    private int _scoreValue = 10;
    private float _speed = 3f;

    [SerializeField]
    private EDirection _direction = EDirection.LEFT;

    [SerializeField]
    private float _jumpForce = 1f;

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
        _rigidbody.velocity = Vector2.zero;
        GetComponent<Animator>().speed = 0;
        _rigidbody.gravityScale = 0;
        this.enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _speed = Random.Range(_fallingSpeedRange.x, _fallingSpeedRange.y);
        Invoke("DestroySelf", 20);
        StartCoroutine("Fall");
        StartCoroutine("Jump");
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(_timeBeforeFall);
        _rigidbody.velocity = new Vector2((_direction == EDirection.RIGHT ? _speed : _speed * -1), 0);
    }

    IEnumerator Jump()
    {
        while (true)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Score.Instance.Add(_scoreValue);
            Destroy(gameObject, 2f);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
        }
    }
}


