using UnityEngine;
using System;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// This class gets command 'Space' 
    /// </summary>
    public class InputController : MonoBehaviour
    {
        private KeyCode _codeAction = KeyCode.Space;

        /// <summary>
        /// Triggers an action when a specific key is pressed
        /// </summary>
        public event Action onKeyDownAction = () => { };

        private void Update()
        {
            if (Input.GetKeyDown(_codeAction))
                onKeyDownAction.Invoke();
        }
    }

}