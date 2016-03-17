using UnityEngine;
using System.Collections;

public class PlatformFollower : MonoBehaviour {

    [SerializeField]
    private GameObject _gameobject;

    [SerializeField]
    private Vector2 _offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + _offset.x, _gameobject.transform.position.y + _offset.y, transform.position.z);
	}
}
