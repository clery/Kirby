using UnityEngine;
using System.Collections;

public class Stalactite : MonoBehaviour {

    [SerializeField]
    private float _timeBeforeFall = 3f;
    [SerializeField]
    private Vector2 _fallingSpeedRange;
    private float _speed = 5f;
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
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        _speed = Random.Range(_fallingSpeedRange.x, _fallingSpeedRange.y);
        Invoke("DestroySelf", 20);
        StartCoroutine("Fall");
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(_timeBeforeFall);
        _rigidbody.velocity = new Vector2(0, _speed);
    }
}
