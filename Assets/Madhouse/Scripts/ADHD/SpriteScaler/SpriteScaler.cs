using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found!");
            return;
        }

        var spriteSize = _spriteRenderer.sprite.bounds.size;
        var screenSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2);
        var scale = screenSize / spriteSize;
        transform.localScale = new Vector3(scale.x, scale.y, 1);
    }
}
