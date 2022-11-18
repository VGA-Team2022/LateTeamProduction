using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>残り時間</summary>
    [SerializeField] float _timeLimit = 300;

    float _clearTime = 0.0f;

    [SerializeField] UIManager _uIManager = default;
    /// <summary>残りの敵（枕）の数</summary>
    [SerializeField] int _sleepingEnemy = 0;
    /// <summary>ゲームオーバー時に表示するUI </summary>
    [SerializeField] GameObject _gameOverUI = null;

    PlayerController _player = default;
    public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
    public PlayerController Player { get => _player; }

    MapInstance _mapInstance = default;
    // Start is called before the first frame update
    void Start()
    {
        //_mapInstance = 
        //_sleepingEnemy = _mapInstance.Entity.SleeperValue;
        _clearTime = _timeLimit;
        _player = FindObjectOfType<PlayerController>();
        _gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if(_sleepingEnemy == 0)
        {
            Clear();
        }
    }

    private void Clear()
    {
        _clearTime -= _timeLimit;
        //Scene移動用関数を使用
    }

    private void Timer()
    {
        _timeLimit -= Time.deltaTime;
        _uIManager.TimerText(_timeLimit);
    }
    public void GameOver()
    {
        _gameOverUI.SetActive(true);
    }
}
