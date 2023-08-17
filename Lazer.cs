using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float _speed = 15f;

    private float _playBoundY = 14;

    private int _damageDealt = 20;
    [SerializeField]
    private float _minFireRate = 30;
    [SerializeField]
    private float _maxFireRate = 50f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
        CalculateDeSpawn();
    }

    public void SetMinFireRate(float minFireRate)
    {
        _minFireRate = minFireRate;
    }

    public void SetMaxFireRate(float maxFireRate)
    {
        _maxFireRate = maxFireRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(_damageDealt);
            }
        }
        if (other.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(0, 1, 0) * _speed * Time.deltaTime;
        transform.Translate(direction);
    }

    private void CalculateDeSpawn()
    {
        if (transform.position.y > _playBoundY)
        {
            Destroy(gameObject);
        }
    }
}
