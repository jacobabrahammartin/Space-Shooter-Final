using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 5f;
    private float _playBoundYTop = 8f;
    private float _playBoundYBottom = -6f;
    private float _playBoundX = 10;

    private float _health = 50f;

    private int _damageDealt = 1;
    private int _score = 10;

    private UIManager _ui;

    private Animator _animator;

    [SerializeField]
    private AudioSource _deathAudio;

    [SerializeField]
    private GameObject _enemyFire;

    private float _minFireRate = 3f;
    private float _maxFireRate = 7f;

    // Start is called before the first frame update
    private void Start()
    {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
        _deathAudio = GetComponent<AudioSource>();
        StartCoroutine(FireRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null && _damageDealt != 0)
            {
                player.Damage(_damageDealt);
                DestroyEnemy();
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minFireRate, _maxFireRate));
            SpawnEnemyLaser();
        }
    }

    private void SpawnEnemyLaser()
    {
        if (_damageDealt != 0)
        {
            Instantiate(_enemyFire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0) * _speed * Time.deltaTime;
        transform.Translate(direction);

        if (transform.position.y < _playBoundYBottom && _damageDealt != 0)
        {
            transform.position = new Vector3(Random.Range(-1 * _playBoundX, _playBoundX), _playBoundYTop, transform.position.z);
        }
    }

    public void Damage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        if (_damageDealt != 0)
        {
            _damageDealt = 0;
            _ui.AddToScore(_score);
            _animator.SetTrigger("onEnemyDeath");
            _deathAudio.Play(0);
            Destroy(gameObject, 2.5f);
        }
    }
}
