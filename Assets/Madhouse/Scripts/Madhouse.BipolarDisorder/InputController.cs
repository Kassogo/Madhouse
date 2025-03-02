using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color whiteColor = Color.white;
    private Color blackColor = Color.black;
 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = whiteColor; // Начальный цвет - белый    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.color = (spriteRenderer.color == whiteColor) ? blackColor : whiteColor;
        }
    }
}
//TODO: fix
