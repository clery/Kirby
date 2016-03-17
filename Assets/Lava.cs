using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

    [SerializeField]
    private float _speed = -2f;

    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(0, _speed);
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
