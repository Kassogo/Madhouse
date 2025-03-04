using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Madhouse.BipolarDisorder
{

    public class InputController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _whiteColor = Color.white;
        private Color _blackColor = Color.black;
 
   
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _whiteColor;   
        }

   
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _spriteRenderer.color = (_spriteRenderer.color == _whiteColor) ? _blackColor : _whiteColor;
            }
        }
    }

}
