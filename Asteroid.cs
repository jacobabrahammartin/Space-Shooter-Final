using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _rotationSpeed = 20.0f;

    private EnemySpawner _enemySpawner;
    private PowerUpSpawner _powerUpSpawner;

    private bool _gameBegun = false;

    [SerializeField]
    private AudioSource _deathAudio;

    // Start is called before the first frame update
    private void Start()
    {
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        _powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        _deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer" && !_gameBegun)
        {
            _gameBegun = true;
            GameObject explosion = Instantiate(_explosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            _deathAudio.Play(0);

            _enemySpawner.StartSpawning();
            _powerUpSpawner.StartSpawning();
            Destroy(gameObject, 1.0f);
        }
    }
}
