using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.5f;
    private float _playBoundY = -6f;

    [SerializeField]
    private AudioSource _powerUpAudio;

    // Start is called before the first frame update
    private void Start()
    {
        _powerUpAudio = GetComponent<AudioSource>();
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
            _powerUpAudio.Play(0);
        }
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0) * _speed * Time.deltaTime;
        transform.Translate(direction);
        CalculateDeSpawn();
    }

    private void CalculateDeSpawn()
    {
        if (transform.position.y < _playBoundY)
        {
            Destroy(gameObject);
        }
    }
}
