using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _coinValue;

    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(3f);
        Deactivate();
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
