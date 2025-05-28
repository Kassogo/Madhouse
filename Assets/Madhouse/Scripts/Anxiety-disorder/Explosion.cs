using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float timer;

    private void OnEnable()
    {
        timer = 2;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
