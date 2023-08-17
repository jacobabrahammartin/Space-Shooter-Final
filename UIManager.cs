using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private int _highScore = 0;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _highScoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image[] _livesImages;

    [SerializeField]
    private Sprite[] _liveSprites;

    private bool _flicker = false;

    private bool _isGameOver = false;

    private GameManager _gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        _scoreText.text = "Score: " + _score;
        _highScoreText.text = "Best: " + _highScore;

        foreach (Image livesImage in _livesImages)
        {
            livesImage.sprite = _liveSprites[_liveSprites.Length - 1];
        }

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator FlickerText()
    {
        while (true)
        {
            _flicker = !_flicker;
            _gameOverText.gameObject.SetActive(_flicker);
            _restartText.gameObject.SetActive(!_flicker);
            yield return new WaitForSeconds(1);
        }
    }

    public void SetLives(int amount, int player)
    {
        if (player - 1 >= 0 && amount >= 0)
        {
            _livesImages[player - 1].sprite = _liveSprites[amount];
            if (amount == 0)
            {
                _gameManager.GameOver();
            }
        }
    }

    public void GameOverScreen()
    {
        StartCoroutine(FlickerText());
    }

    public void AddToScore(int amount)
    {
        _score += amount;
        _scoreText.text = "Score: " + _score;
        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.text = "Best: " + _highScore;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }

    public void UpdateHighScore(int newHighScore)
    {
        _highScore = newHighScore;
        _highScoreText.text = "Best: " + _highScore;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
}
