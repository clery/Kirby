using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector2 _minPosition;
    [SerializeField]
    private Vector2 _maxPosition;

    // Use this for initialization
    void Start () {

        _maxPosition.x *= Camera.main.pixelWidth / 100;
        _maxPosition.y *= Camera.main.pixelWidth / 100;
        _minPosition.x *= Camera.main.pixelWidth / 100;
        _minPosition.y *= Camera.main.pixelWidth / 100;
    }

    // Update is called once per frame
    void Update () {
        transform.position = _target.position;
        if (transform.position.x < _minPosition.x)
            transform.position = new Vector2(_minPosition.x, transform.position.y);
        if (transform.position.y < _minPosition.y)
            transform.position = new Vector2(transform.position.x, _minPosition.y);
        if (transform.position.x > _maxPosition.x)
            transform.position = new Vector2(_maxPosition.x, transform.position.y);
        if (transform.position.y > _maxPosition.y)
            transform.position = new Vector2(transform.position.x, _maxPosition.y);
        transform.position -= new Vector3(0, 0, 10);
    }
}
