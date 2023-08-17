using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver = false;

    [SerializeField]
    private int _numberOfPlayersAlive = 1;
    private int _numberOfPlayers = 1;

    private UIManager _uiManager;

    private EnemySpawner _enemySpawner;
    private PowerUpSpawner _powerUpSpawner;

    [SerializeField]
    private Canvas _pauseMenu;

    // Start is called before the first frame update
    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        _powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        _numberOfPlayers = _numberOfPlayersAlive;
        _powerUpSpawner.StartSpawning();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForGameRestart();
    }

    private void CheckForGameRestart()
    {
        if (Input.GetKeyDown(KeyCode.R) && IsGameOver)
        {
            if (_numberOfPlayers == 1)
            {
                SceneManager.LoadScene(1);
            }
            else if (_numberOfPlayers == 2)
            {
                SceneManager.LoadScene(2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void GameOver()
    {
        _numberOfPlayersAlive--;
        if (_numberOfPlayersAlive == 0)
        {
            IsGameOver = true;
            _enemySpawner.StopSpawning();
            _powerUpSpawner.StopSpawning();
            _uiManager.GameOverScreen();
        }
    }

    public void Pause()
    {
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
