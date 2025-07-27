using Madhouse.AnxietyDisorder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeakEnemy : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _enemyVisual;
    [SerializeField] private GameObject _enemyDamageVisual;
    [SerializeField] private GameObject _panelGO;

    [SerializeField] GameObject mainCamera;

    private SpriteRenderer _spriteRenderer;

    private Transform _target;
    private bool _canMove;
    private float _direction;

    private void OnEnable()
    {
        transform.position = spawnPoint.position;

        _target = GameObject.FindGameObjectWithTag("BrainCentre").transform;
        _canMove = true;
        _direction = 1;

        _enemyVisual.SetActive(true);
        _enemyDamageVisual.SetActive(false);

        _spriteRenderer = _enemyVisual.GetComponent<SpriteRenderer>();

        StartCoroutine(StartFade());
    }

    private void FixedUpdate()
    {
        if (_canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * _direction * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            _canMove = true;
            _direction = -1;
        }
        else if (collision.gameObject.CompareTag("Brain"))
        {
            _canMove = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brain"))
        {
            _canMove = true;
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            _direction = 1;
        }
    }

    private void OnMouseDown()
    {
        if (Bonuses.instance.mindBreathOn == false)
        {
            ExplosionPool.instance.CreateExplosion(transform.position, 0.1f);
            HealthBar.instance.gameProcess = false;
            _panelGO.SetActive(true);
            Panel.instance.StartFadeIn();
            this.Deactivate();
        }
    }

    private void OnMouseEnter()
    {
        if (Bonuses.instance.mindBreathOn == true)
        {
            StartCoroutine(DamageVisual());
            ExplosionPool.instance.CreateExplosion(transform.position, 0.1f);
            HealthBar.instance.gameProcess = false;
            _panelGO.SetActive(true);
            Panel.instance.StartFadeIn();
            this.Deactivate();
        }
    }

    private void Deactivate()
    {
        mainCamera.GetComponent<SC_CursorTrail>().enabled = false;
        SoundManager.instance.WeakEnemyDie();
        this.gameObject.SetActive(false);
    }

    private IEnumerator DamageVisual()
    {
        _enemyDamageVisual.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _enemyDamageVisual.SetActive(false);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator StartFade()
    {
        float alphaVal = _spriteRenderer.color.a;
        Color _color = _spriteRenderer.color;

        while (_spriteRenderer.color.a < 1)
        {
            alphaVal += 0.05f;
            _color.a = alphaVal;
            _spriteRenderer.color = _color;

            yield return new WaitForSeconds(0.1f);
        }
    }
}