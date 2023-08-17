using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 13f;

    private float _playBoundX = 11;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    [SerializeField]
    private GameObject _fireRight;

    [SerializeField]
    private GameObject _leftFire;

    [SerializeField]
    private GameObject _thruster;

    private float _nextFireRate = 0.25f;

    private int _health = 3;

    private bool _trippleShot = false;
    private bool _shield = false;
    private float _speedMultiplier = 1f;
    private float _speedBoost = 1.2f;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private AudioSource _deathAudio;

    [SerializeField]
    private AudioSource _powerUpAudio;

    private UIManager _ui;

    private Animator _animator;

    [SerializeField]
    private int _player = 1;

    // Start is called before the first frame update
    private void Start()
    {
        if (_player == 1)
        {
            transform.position = new Vector3(0, -4, 0);
        }
        else if (_player == 2)
        {
            transform.position = new Vector3(3, -4, 0);
        }

        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _deathAudio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUpTrippleShot")
        {
            Destroy(other.gameObject, 0.1f);
            EnableTrippleShot();
        }
        if (other.tag == "PowerUpSpeed")
        {
            Destroy(other.gameObject, 0.1f);
            EnableSpeedMultiplier();
        }
        if (other.tag == "PowerUpShield")
        {
            Destroy(other.gameObject, 0.1f);
            EnableShield();
        }
    }

    private void Fire()
    {
        if (((_player == 1 && Input.GetKey(KeyCode.Space)) || (_player == 2 && Input.GetKey(KeyCode.Keypad0))) && Time.time > _nextFireRate)
        {
            _nextFireRate = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

            if (_trippleShot)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.5f, 0f, 0), Quaternion.Euler(0, 0, 15));
                Instantiate(_laserPrefab, transform.position + new Vector3(0.5f, 0f, 0), Quaternion.Euler(0, 0, -15));
            }
        }
    }

    private void CalculateMovement()
    {
        float inputH = 0;
        float inputV = 0;

        if (_player == 1)
        {
            inputH = Input.GetAxis("HorizontalP1");
            inputV = Input.GetAxis("VerticalP1");
        }
        else if (_player == 2)
        {
            inputH = Input.GetAxis("HorizontalP2");
            inputV = Input.GetAxis("VerticalP2");
        }

        Vector3 direction = new Vector3(inputH, inputV, 0) * _speed * _speedMultiplier * Time.deltaTime;
        transform.Translate(direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), transform.position.z);

        if (transform.position.x > _playBoundX)
        {
            transform.position = new Vector3(-1 * _playBoundX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -1 * _playBoundX)
        {
            transform.position = new Vector3(1 * _playBoundX, transform.position.y, transform.position.z);
        }
    }

    public void Damage(int amount)
    {
        if (_shield)
        {
            _shield = false;
            _shieldVisualizer.SetActive(false);
        }
        else
        {
            UpdateHealth(-1);
            _trippleShot = false;
            _speedMultiplier = 1f;

            if (_health <= 0)
            {
                _deathAudio.Play(0);
                _animator.SetTrigger("onDeath");
                Destroy(gameObject, 1.5f);
                _leftFire.SetActive(false);
                _fireRight.SetActive(false);
                _thruster.SetActive(false);
            }

            if (_health == 2)
            {
                _fireRight.SetActive(true);
            }
            else if (_health == 1)
            {
                _leftFire.SetActive(true);
            }
        }
    }

    public void EnableTrippleShot()
    {
        _trippleShot = true;
    }

    public void EnableShield()
    {
        _shield = true;
        _shieldVisualizer.SetActive(true);
    }

    public void EnableSpeedMultiplier()
    {
        _speedMultiplier = _speedBoost;
    }

    public void AddScore(int amount)
    {
        _ui.AddToScore(amount);
    }

    public void UpdateHealth(int amount)
    {
        _health += amount;
        _ui.SetLives(_health, _player);
    }
}
