using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerUpPrefabs;

    private bool _spawn = true;

    private float _playBoundYTop = 8f;
    private float _playBoundX = 10;

    private float _nextSpawnRate = 15f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_nextSpawnRate + Random.Range(0, _nextSpawnRate * 0.5f));
        while (_spawn)
        {
            int numberOfPowerUpsToSpawn = Random.Range(1, 4); // Randomly spawn 1 to 3 power-ups
            for (int i = 0; i < numberOfPowerUpsToSpawn; i++)
            {
                Spawn();
            }
            yield return new WaitForSeconds(_nextSpawnRate + Random.Range(0, _nextSpawnRate));
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Spawn()
    {
        Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Length)], new Vector3(Random.Range(-1 * _playBoundX, _playBoundX), _playBoundYTop, transform.position.z), Quaternion.identity);
    }

    public void StopSpawning()
    {
        _spawn = false;
    }
}
