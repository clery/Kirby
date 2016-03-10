using UnityEngine;
using System.Collections;

public class BackgroundFitter : MonoBehaviour {

    [SerializeField]
    private Camera _mainCamera;
    private Sprite _sprite;

	// Use this for initialization
	void Start () {
        _sprite = GetComponent<SpriteRenderer>().sprite;
        Debug.Log("Background : " + _sprite.rect.size.x + "x" + _sprite.rect.size.y);
        if (_sprite.rect.size.x > _sprite.rect.size.y)
            if (_mainCamera.pixelWidth > _mainCamera.pixelHeight)
                transform.localScale = new Vector2(_mainCamera.pixelWidth / _sprite.rect.size.x, _mainCamera.pixelWidth / _sprite.rect.size.x);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
