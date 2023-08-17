using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    private float _speed = 15f;
    private float _playBoundYBottom = -14f;

    private int _damageDealt = 1;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CalculateDeSpawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(_damageDealt);
            }
        }
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0) * _speed * Time.deltaTime;
        transform.Translate(direction);
    }

    private void CalculateDeSpawn()
    {
        if (transform.position.y < _playBoundYBottom)
        {
            Destroy(gameObject);
        }
    }
}
