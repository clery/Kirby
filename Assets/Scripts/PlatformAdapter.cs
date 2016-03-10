using UnityEngine;
using System.Collections;

public class PlatformAdapter : MonoBehaviour {

    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Vector2 _position;
    [SerializeField]
    private Vector2 _size;

    // Use this for initialization
    void Start () {
        if (_position.x != 0)
            transform.localPosition = new Vector2(_position.x * (_mainCamera.pixelWidth) / 100, transform.localPosition.y);
        if (_position.y != 0)
            transform.localPosition = new Vector2(transform.localPosition.x, _position.y * (_mainCamera.pixelHeight) / 100);

        if (_size.x != 0)
            transform.localScale = new Vector2(_size.x * _mainCamera.pixelWidth / 10, transform.localScale.y);
        if (_size.y != 0)
            transform.localScale = new Vector2(transform.localScale.x, _size.y * _mainCamera.pixelHeight / 10);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
